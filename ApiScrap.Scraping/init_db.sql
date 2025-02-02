CREATE TABLE usuario (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    correo VARCHAR(255) NOT NULL,
    usuario VARCHAR(255) NOT NULL,
    clave VARCHAR(255) NOT NULL,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    usuario_registro VARCHAR(255) NOT NULL
);

CREATE TABLE archivo (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    procesado BOOLEAN DEFAULT FALSE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    usuario_registro VARCHAR(255) NOT NULL
);

CREATE TABLE direccion_url (
    id SERIAL PRIMARY KEY,
    url TEXT NOT NULL,
    procesado BOOLEAN DEFAULT FALSE,
    archivo_id INT REFERENCES archivo(id)
);

CREATE TABLE detalle_direccion (
    id SERIAL PRIMARY KEY,
    etiqueta VARCHAR(50) NOT NULL,
    contenido TEXT NOT NULL,
    url_id INT REFERENCES direccion_url(id)
);
