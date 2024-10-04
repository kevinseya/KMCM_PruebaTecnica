# KMCM Prueba Técnica

## Descripción
Este proyecto es una API desarrollada en ASP.NET Core 7 para gestionar usuarios y personas. El frontend utiliza Angular para interactuar con la API.

## Requisitos
- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js (versión recomendada)](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [MongoDB](https://www.mongodb.com/try/download/community)

## Dependencias
### Backend (.NET Core API)
Las siguientes dependencias están incluidas en el proyecto:
- Microsoft.AspNetCore.Authentication.JwtBearer (7.0.20)
- Microsoft.AspNetCore.OpenApi (7.0.15)
- Microsoft.EntityFrameworkCore (7.0.20)
- Microsoft.EntityFrameworkCore.Design (7.0.20)
- Microsoft.EntityFrameworkCore.SqlServer (7.0.20)
- Microsoft.EntityFrameworkCore.Tools (7.0.20)
- Newtonsoft.Json (13.0.3)
- Serilog (4.0.2)
- Serilog.AspNetCore (8.0.2)
- Serilog.Extensions.Logging (8.0.0)
- Serilog.Sinks.Console (6.0.0)
- Serilog.Sinks.MongoDB (6.0.0)
- Swashbuckle.AspNetCore (6.5.0)

### Frontend (Angular)
El frontend está desarrollado con Angular. Asegúrate de tener la versión compatible de Node.js y Angular CLI instalados.

Requisitos de Instalación

    Node.js: Se recomienda usar Node.js 16.x o 18.x.
    Angular: Para este proyecto se uso Angular 16.1.0. La versión de Angular CLI utilizada es 16.1.8.

Dependencias

Aquí hay un resumen de las principales dependencias y sus versiones:

    Angular:
        @angular/animations: ^16.1.0
        @angular/common: ^16.1.0
        @angular/compiler: ^16.1.0
        @angular/core: ^16.1.0
        @angular/forms: ^16.1.0
        @angular/platform-browser: ^16.1.0
        @angular/platform-browser-dynamic: ^16.1.0
        @angular/router: ^16.1.0

    Otros:
        crypto-js: ^4.2.0
        rxjs: ~7.8.0
        zone.js: ~0.13.0

    DevDependencies:
        @angular-devkit/build-angular: ^16.1.8
        @angular/compiler-cli: ^16.1.0
        typescript: ~5.1.3
        
## Configuración

### Backend - RAMA *kmcm_backend*
1. **Clonar el repositorio**:
   ```Terminal
   git clone <https://github.com/kevinseya/KMCM_PruebaTecnica.git>
   cd <NOMBRE_DEL_DIRECTORIO
2. **Instalar dependencias**:
    ```Terminal
   dotnet restore
4. **Configurar appsettings.json: Asegúrate de configurar la cadena de conexión a tu base de datos SQL Server y MongoDB en el archivo appsettings.json**:
      ```
        {
        "Logging": {
          "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
          }
        },
        "ConnectionStrings": {
          "Kmcm_SqlServer": "Server=TUSERVIDOR*;Database=Kmcm_Database;User Id=TUSUARIO*; Password=TUPASSWORD*;TrustServerCertificate=True",
          "Kmcm_MongoDB": "mongodb://localhost:TUPUERTO*/logs"
        },
        "Kmcm_PathLog": "C:\\logs\\logs_Kmcm_PruebaTecnica.txt",
        "AllowedHosts": "*",
        "Jwt": {
          "Key": "7vhY1FbB5xV6QW1R3lEc64UpK+z5YlX2Ib8QHIc0ZwI= **O LA KEY QUE DESEES",
          "Issuer": "https://localhost:7240 **O EL PUERTO QUE DESEES",
          "Audience": "https://localhost:7240 **O EL PUERTO QUE DESEES"
        },
        "EncryptionSettings": {
          "Key": "12345678901234567890123456789012 **O LA KEY QUE DESEES",
          "IV": "1234567890123456 **O LA IV QUE DESEES"
        },
        "Ports": {
          "http": "http://localhost:5200 **O EL PUERTO QUE DESEES"",
          "https": "https://localhost:7240 **O EL PUERTO QUE DESEES""
        }
      }

5. **Ejecutar migraciones y crear base de datos**:
    ```Terminal
   dotnet ef migrations add *NOMBRE DE LA MIGRACION
   dotnet ef database update
   
### FRONTEND - RAMA *kmcm_frontend*
1. **Clonar el Reposiorio**:
    ```Terminal
    git clone <https://github.com/kevinseya/KMCM_PruebaTecnica.git>
    cd kmcm_frontend
2. **Instalar dependencias**:
    ```Terminal
    npm install
3. **Ejecutar la aplicación**:
    ```Terminal
    ng server



## DESPLIEGUE

**Para desplegar la aplicación, sigue estos pasos**:
**Backend**
  *Publicar la aplicación*:

      dotnet publish -c Release
  *Copiar los archivos de publicación al servidor donde se ejecutará la API*.
  *Configurar el servidor (IIS o Kestrel) para apuntar a la carpeta de publicación*.

**Frontend**
   *Construir la aplicación*:

    ng build --prod

  *Copiar los archivos de dist al servidor web donde se hospedará el frontend*.


### Notas Adicionales:
- Recuerda que para poder ingresar, una vez creada la Base de datos debes insertar una persona y su respectivo usuario para empezar a probar:
 ```
INSERT INTO [KMCM_Persons] (KMCM_NAME, KMCM_LASTNAME, KMCM_ADDRESS, KMCM_PHONE, KMCM_BIRTHDATE)
VALUES ('ADMIN', 'PRUEBA', 'PRUEBA_DIRECCION', '0999999999', '2000-01-01');

INSERT INTO [KMCM_Users] (KMCM_USERNAME, KMCM_PASSWORD, kmcm_person_id)
VALUES ('admin', '38GLTXFinSFH3yOiXwZdyw==', (SELECT KMCM_ID_PERSON FROM [KMCM_Persons] WHERE KMCM_NAME = 'ADMIN' AND KMCM_LASTNAME = 'PRUEBA'));
