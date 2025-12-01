
# GRPC APi en Python

Esta es una Api Grpc desarrollada en python, la cual registra usuarios (cartografos) dentro de una base de datos (MongoDB) y permite realizar las operaciones CreateCartographer(client streaming), GetByID(unary call) y GetByName(server streaming).


## Instrucciones

Para realizar las pruebas es necesario dentro de la carpeta de la api rest realizar el comando mvn clean compile para asegurar que las clases generadas se creen correctamente y evitar problemas al crear los contenedores.

Ejecutar el docker-compose con el comando podman compose up --build -d generando los contenedores de los tres proyectos

El contenedor de grpc corre en el puerto 50551
El contenedor de rest corre en el puerto 8083

Para probar cada endpoint en grpc importar el proto en postman y seleccionar el endpoint deseado

Create example message
{
    "age": 19,
    "company": "velit",
    "name": "pepe"
}

GetById example message
{
    "id": "692d08a56b9ddbfe97a059f6"
}

GetByName example message
{
    "name": "a"
}

Para probar los endpoints implementados desde el rest se utiliza

localhost:8083/cartographers

Create example
POST localhost:8083/cartographers

GetByID example
GET localhost:8083/cartographers/id/692d08a56b9ddbfe97a059f6

GetByName example
GET localhost:8083/cartographers/by-name/?name=j