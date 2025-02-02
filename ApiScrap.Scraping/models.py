from sqlalchemy import Column, Integer, String, Boolean, TIMESTAMP, Text, ForeignKey, func
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()

class Usuario(Base):
    __tablename__ = 'usuario'
    id = Column(Integer, primary_key=True, index=True)
    nombre = Column(String, nullable=False)
    correo = Column(String, nullable=False)
    usuario = Column(String, nullable=False)
    clave = Column(String, nullable=False)
    fecha_registro = Column(TIMESTAMP, server_default=func.now())
    usuario_registro = Column(String, nullable=False)


class Archivo(Base):
    __tablename__ = 'archivo'
    id = Column(Integer, primary_key=True, index=True)
    nombre = Column(String, nullable=False)
    procesado = Column(Boolean, default=False)
    fecha_registro = Column(TIMESTAMP, server_default=func.now())
    usuario_registro = Column(String, nullable=False)

class DireccionUrl(Base):
    __tablename__ = 'direccion_url'
    id = Column(Integer, primary_key=True, index=True)
    url = Column(String, nullable=False)
    procesado = Column(Boolean, default=False)
    archivo_id = Column(Integer, ForeignKey('archivo.id'))

class DetalleDireccion(Base):
    __tablename__ = 'detalle_direccion'
    id = Column(Integer, primary_key=True, index=True)
    etiqueta = Column(String, nullable=False)
    contenido = Column(Text, nullable=False)
    url_id = Column(Integer, ForeignKey('direccion_url.id'))
