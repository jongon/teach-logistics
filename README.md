## Synopsis

TeachLogistics es un software educativo de gestión de inventario principalemnte dirigidos a estudiantes que estén interesados en aprender del tema de cadena de suministros
Desarrollado en .NET c#, Asp MVC 5

## Motivación

El trabajo especial de grado que al final terminó siendo mención publicación y mención honorífica xD (Yeiiiiiii)

## Manual de Usuario
[Descargar Manual Usuario Alumno](https://mega.nz/#!p1MgmQgD!j07M9nXRlKYVOZJ3zb3w-a9gJoRVRPC98op9bRSqAX4)
[Descargar Manual Usuario Profesor](https://mega.nz/#!ppt1BQ5a!8KM1lLN9gIfVJWdov5-eekYLh40TvxTXLbB9VTisHxI)

## Instalación

 - No olvides instalar las dependencias Nuget
 - Es necesario tener una base de datos MySQL instalada en el sistema, aquí un ejemplo de la configuración del "connection string" a modificar en el we.config del proyecto

  <connectionStrings>
    <add name="DefaultConnection" connectionString="Server=localhost;Database=teachlogistics;Uid=root;Pwd=root;" providerName="MySql.Data.MySqlClient"/>
  </connectionStrings>

## Pruebas

- El proyecto posee pruebas unitarias para el módulo de Estadísticas y Resultados, 
puede ser que requiera data en el sistema para realizar las pruebas
- Se pueden hacer perfectamente en la suite de Visual Studio

## Diagramas

- En el proyecto se encuentran los distintos diagramas de clases y diagramas de secuencias

## Licencia

Copyright (c) 2015 Universidad Católica Andrés Bello

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.