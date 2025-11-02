
# MapApi Rest

Esta es una api rest que desarrolle utilizando spring boot y java, lo que realiza la api es consumir la api soap realizada en el parcial pasado para utilizar los endpoints de la api anterior, la api implementa cache con redis para el endpoint get 


## Instrucciones de la api

Esta version implementa solo los endpoints GetById y CreateCountry, ya que eran los solicitados el parcial anterior
No pude crear exitosamente el compose para este proyecto, por lo que fue probado de manera local.

Api rest: puerto 8043
Api soap: puerto 8080
countries-db: puerto 3306

Los endpoints de la api rest pueden ser probados mediante postman

localhost:8043/country para probar el Create

{
    "name": "Mexico",
    "capital": "CDMX",
    "population": 204694,
    "currency": "MXN"
}

localhost:8043/country/{id} para probar el GetById

localhost:8043/country/1


