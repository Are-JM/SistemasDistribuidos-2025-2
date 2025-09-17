
# CountriesApi Soap

Esta es una API SOAP que permite agregar registros a una base de datos de paises y buscarlos mediante su id, se realizó con java y utilizando el framework de spring boot y maven para construir el proyecto, se utilizó flyway para realizar la migración de la base de datos y la base de datos fue creada con mysql



## Instrucciones para correr la api

-Primero

Si la carpeta target se encuentra dentro del proyecto es recomendable eliminarla y correr el siguiente comando en la terminal(estando ya en la carpeta demo, donde se encuentra la api)

 `mvn clean package -DskipTests` 
 
 para evitar conflictos al momento de crear la imagen que usaremos en el contenedor

-Segundo

En el archivo aplication.properties que se encuentra dentro de resources se establece la conexión con la base de datos, para evitar problemas con la conexión con la bd es necesario modificar el usuario y contraseña establecidos.

-Contenedores

Para crear los contenedores se usa docker o podman 

Primero se crea un network

`podman network create country-net` 

`docker network create country-net`

Para crear el conenedor de la base de datos

` podman run -d --name countries-db --network country-net -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=countries_db -
p 3311:3306 mysql:8.1.0 `

Para crear la imagen de la api

`podman build -t countries-api:1 .`

Para crear el contenedor de la api

`podman run -d --name countries-api --network country-net -p 8080:8080  countries-api:1`

-Pruebas

Para probar la api en postman usar el siguiente url

`http://localhost:8080/ws/countries.wsdl`