# ⏱️ Horas Extra - Backend

Este proyecto es una aplicación **backend** para el **registro, gestión y cálculo de horas extra** realizadas por empleados, diseñada para integrarse en entornos empresariales. Este proyecto está compuesto por tres servicios principales: una base de datos PostgreSQL, un backend desarrollado en .NET y un frontend basado en una aplicación de dashboard. La infraestructura está configurada utilizando Docker y Docker Compose para facilitar la implementación y el desarrollo.


## 🚀 Características principales

- Ingreso de registros de horas trabajadas.
- Cálculo automático de horas extra según reglas configurables.
- Notificaciones y envío de reportes por correo.
- Arquitectura modular: API, Core y Capa de Infraestructura.
- Soporte para pruebas unitarias con **XUnit**.
- Conexión con chatbot para ingreso de horas por los empleados.
- Extracción de días feriados desde sitio web.

## 🧱 Estructura del proyecto
**Flujo del empleado**
![1](https://github.com/user-attachments/assets/515b95c9-f751-4f5c-8341-c5564dcabb24)

**Flujo administrador**
![2](https://github.com/user-attachments/assets/8fb857e3-4b7f-43ea-8952-52c42d36aecd)

## <img width="590" alt="image" src="https://github.com/user-attachments/assets/ee03ba8d-5f73-492c-abf0-b0c2bacd0ff3" />

El proyecto está organizado en los siguientes directorios:

- **ExtraHours.Api/**: Contiene el código fuente del backend desarrollado en .NET.
- **ExtraHours.Core/**: Incluye los modelos, servicios y repositorios principales del proyecto.
- **ExtraHours.Infrastructure/**: Contiene la configuración de acceso a datos y otras dependencias de infraestructura.
- **ExtraHoursTest/**: Directorio para pruebas unitarias y de integración.
- **amadeus-dashboard/**: Contiene el código fuente del frontend.

🛠️ Tecnologías utilizadas

- **.NET** (C#)
- **ASP.NET Web API**
- **Entity Framework Core**
- **Postgres** (base de datos)
- **Docker** y **Docker Compose**
- **XUnit** (para pruebas)

## Requisitos

- Docker (versión 20.10 o superior)
- Docker Compose (versión 1.29 o superior)
- .NET SDK (versión 6.0 o superior)

### Servicios

1. **Base de datos**:
   - Imagen: `postgres:15`
   - Puerto: `5432`
   - Variables de entorno:
     - `POSTGRES_USER`: Usuario de la base de datos.
     - `POSTGRES_PASSWORD`: Contraseña de la base de datos.
     - `POSTGRES_DB`: Nombre de la base de datos.
   - Volumen: `pgdata:/var/lib/postgresql/data`

2. **backend**:
   - Dockerfile: [`ExtraHours.Api/Dockerfile`](ExtraHours.Api/Dockerfile)
   - Puerto: `5011`
   - Variables de entorno:
     - `ConnectionStrings__DefaultConnection`: Cadena de conexión para la base de datos PostgreSQL.

3. **frontend**:
   - Dockerfile: [`amadeus-dashboard/Dockerfile`](amadeus-dashboard/Dockerfile)
   - Puerto: `5137`
   - Dependencias: `backend`


## Pasos para Ejecutar el Proyecto

1. Clona el repositorio:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd ExtraHoursG1
   ```

2. Construye y ejecuta los servicios con Docker Compose:
   ```bash
   docker-compose up --build
   ```

3. Accede a los servicios:
   - Backend: [http://localhost:5011](http://localhost:5011)
   - Frontend: [http://localhost:5137](http://localhost:5137)

## Estructura del Backend

El backend está desarrollado en .NET y sigue una arquitectura modular. Los principales componentes incluyen:

- **Controllers**: Manejan las solicitudes HTTP.
- **Services**: Contienen la lógica de negocio.
- **Repositories**: Gestionan el acceso a datos.
- **Models**: Representan las entidades del dominio.

## Pruebas

Las pruebas unitarias y de integración están ubicadas en el directorio `ExtraHoursTest`. Para ejecutarlas, utiliza el siguiente comando:

```bash
dotnet test ExtraHours.sln
```

## 🐳 Instrucciones de despliegue (Docker)

```bash
# Construir los contenedores
docker-compose up --build

## Contribución

Si deseas contribuir al proyecto, por favor sigue los siguientes pasos:

1. Crea un fork del repositorio.
2. Crea una rama para tu funcionalidad o corrección.
3. Realiza un pull request describiendo los cambios realizados.

## Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo `LICENSE` para más detalles.
