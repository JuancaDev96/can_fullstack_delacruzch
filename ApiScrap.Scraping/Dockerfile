# Usar una imagen base de Python
FROM python:3.9-slim

# Instalar curl
RUN apt-get update && apt-get install -y curl

# Establecer el directorio de trabajo en el contenedor
WORKDIR /app

# Copiar los archivos requirements.txt y instalar las dependencias
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copiar el código de la aplicación al contenedor
COPY . .

# Crear el directorio de datos
RUN mkdir -p /app/data

# Exponer el puerto en el que se ejecutará la aplicación
EXPOSE 8000

# Comando para ejecutar la aplicación
CMD ["uvicorn", "scraper_api:app", "--host", "0.0.0.0", "--port", "8000"]
