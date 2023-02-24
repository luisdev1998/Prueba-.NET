
1. Aplicación desarrollada en .NET Core 7

2. Para ejecutar el proyecto debe descargar la siguiente paquete: AutoMapper.Extensions.Microsoft.DependencyInjection

3. El proyecto al ejecutarse se mostrará la documentación Swagger.

4. Para almacenar los registros en memoria de la entidad Producto, lo agregué como un servicio "Addsingleton" el cual crea una instancia única compartida que nos servirá como una "Base de datos".

5. Se usó el patrón de diseño "Repository Pattern" cuya clase se encuentra en la carpeta "Repositorio" donde se aprecia la implementación de una capa de abstracción entre la capa de negocio de aplicación y la capa de acceso a datos usando una interfaz.

6. Para el intercambio de información entre cliente y las clases se aplicó lo que es la implementación de (Data transfer objects) mappers. Estos se encuentran configurados en la carpeta "DTOs" y "Utilidades" siguientdo uno de los principios solid (Responsabilidad única)

7. Como api externa para traer el campo de descuento he creado una url desde mi servidor "https://profile.luissanchez.site/prueba/api/prueba.json" el cual se creó un método asincrónico para obtener dicha propiedad desde otro método asincrónico.

8. El logeo de los request se encuentra dentro de la carpeta "wwwroot". Para este caso se ha implementado como un middleware. "app.UseMiddleware<LoggingMiddleware>();"
  
9. El diccionario de datos se agregó dentro de los servicios usando la extensión de microsoft Caching.Memory.
  
10. Se respetó que al registrar un producto, el campo "Status" se muestra como parámetro al realizar un Post request. Al realizar un Get Request, se muestra como "StatusName". Esto se logró gracias a usar los Data Transfer Objects.
  
11. Me faltó agregar la implementación de las pruebas TDD ya que el tema aún lo desconozco.
  
¡ Muchas gracias por dejarme participar en su proceso !
 Agradecería que me hagan saber las recomendaciones para mejorar en mi código.
