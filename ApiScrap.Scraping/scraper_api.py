from fastapi import FastAPI, File, Form, UploadFile, BackgroundTasks, HTTPException, Depends
from sqlalchemy.orm import Session, sessionmaker
from sqlalchemy import create_engine
from pydantic import BaseModel, HttpUrl
import requests
from bs4 import BeautifulSoup
from typing import List
import redis
import celery
import os
from database import get_db, engine, Base  # Importar Base y engine
from models import Archivo, DireccionUrl, DetalleDireccion, Usuario  # Importar todos los modelos

# Configuración de Redis y Celery
redis_client = redis.Redis(host='redis', port=6379, db=0)
celery_app = celery.Celery(
    "tasks",
    broker="redis://redis:6379/0",
    backend="redis://redis:6379/0"
)

app = FastAPI()

# Crear tablas
Base.metadata.create_all(bind=engine)

class ScrapeRequest(BaseModel):
    url: HttpUrl
    tags: List[str]

@app.get("/")
def health_check():
    return {"message": "API is running"}

@celery_app.task
def scrape_data(url: str, tags: List[str], db_url: str, usuario: str):
    response = requests.get(url, timeout=10)
    response.raise_for_status()
    
    soup = BeautifulSoup(response.text, "html.parser")
    scraped_data = {}
    
    for tag in tags:
        data = [element.get_text(strip=True) for element in soup.find_all(tag)]
        scraped_data[tag] = data if data else "No se encontraron elementos con ese tag."
    
    engine = create_engine(db_url)
    SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
    db = SessionLocal()
    
    try:
        direccion_url = db.query(DireccionUrl).filter(DireccionUrl.url == url).first()
        if direccion_url:
            direccion_url.procesado = True
            db.commit()
            
            for tag, content in scraped_data.items():
                for item in content:
                    detalle = DetalleDireccion(
                        etiqueta=tag,
                        contenido=item,
                        url_id=direccion_url.id
                    )
                    db.add(detalle)
            db.commit()

    finally:
        db.close()
    
    return {"url": url, "data": scraped_data}


@app.post("/scrape-file/")
async def scrape_from_file(usuario: str = Form(...), file: UploadFile = File(...), db: Session = Depends(get_db)):
    try:
        content = file.file.read().decode("utf-8")
    except Exception as e:
        raise HTTPException(status_code=400, detail="Error al leer el archivo.")
    
    archivo = Archivo(nombre=file.filename, usuario_registro = usuario)
    db.add(archivo)
    db.commit()
    db.refresh(archivo)
    
    urls = content.splitlines()
    tags = ["p", "h1", "div"]
    
    for url in urls:
        direccion_url = DireccionUrl(url=url, archivo_id=archivo.id)
        db.add(direccion_url)
        db.commit()
        db.refresh(direccion_url)
        
        scrape_data.delay(url, tags, os.getenv('DATABASE_URL'), usuario)
    

    usuario_obj = db.query(Usuario).filter(Usuario.usuario == usuario).first()
    if usuario_obj:
        email_api_url = "http://apigateway:8080/api/email/send"
        email_payload = {
            "asunto": "Scraping Completo",
            "cuerpo": f"El scraping del archivo {file.filename} se ha completado.",
            "destinatario": usuario_obj.correo
        }
            
        try:
            email_response = requests.post(email_api_url, json=email_payload)
            email_response.raise_for_status()
        except requests.RequestException as e:
            print(f"Error al enviar el correo: {e}")

    return {"message": "Scraping iniciado en segundo plano", "total_urls": len(urls)}
