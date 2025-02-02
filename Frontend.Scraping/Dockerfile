# Etapa de construcción
FROM node:18-alpine as build
WORKDIR /app

# Copia los archivos package.json y package-lock.json
COPY package*.json ./
RUN npm ci

# Copia los archivos del proyecto al directorio de trabajo
COPY . ./

# Compila la aplicación de Angular
RUN npm run build --configuration Frontend.Scraping

# Etapa de runtime
FROM node:18-alpine
WORKDIR /usr/app

# Copia los archivos compilados de Angular desde la etapa de construcción
COPY --from=build /app/dist/frontend.scraping /usr/app/dist/frontend.scraping

# Exponer el puerto
EXPOSE 4000

# Comando para iniciar el servidor Node.js
CMD ["node", "dist/frontend.scraping/server/server.mjs"]
