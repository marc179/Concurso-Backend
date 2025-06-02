# 🛍️ RopaStore API – Backend en .NET 7

Este es el backend oficial de la app RopaStore. 
Está desarrollado en .NET 7 con arquitectura en capas, Entity Framework Core y SQL Server. 
Permite gestionar usuarios, productos, categorías, pedidos y sugerencias.

## 🔒 Autenticación

Este backend utiliza JWT (JSON Web Tokens) para autenticación y autorización por roles: Cliente, Vendedor, Administrador.

## 🧱 Arquitectura

- ASP.NET Core Web API
- Clean Architecture (Domain, Application, Infrastructure, API)
- Entity Framework Core
- SQL Server
- JWT para autenticación

## 🚀 ¿Cómo ejecutar este backend?

1. Clona el repositorio:

```bash
git clone https://github.com/marc179/RopaStore-Backend.git
cd RopaStore-Backend

Configura la cadena de conexión:

Edita el archivo appsettings.Development.json en src/RopaStore.API:
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-XXXXX\\SQLEXPRESS;Database=RopaStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
},
"Jwt": {
  "Key": "clave-super-segura-de-al-menos-32-caracteres",
  "Issuer": "RopaStore.API",
  "Audience": "RopaStore.Clientes"
}
Asegúrate de que la propiedad Jwt:Key tenga al menos 32 caracteres.

Crea la base de datos con EF Core:
dotnet ef database update --project src/RopaStore.Infrastructure --startup-project src/RopaStore.API
Corre el servidor:
dotnet run --project src/RopaStore.API --urls "http://0.0.0.0:5295"
Esto permitirá que Flutter (u otro frontend) pueda conectarse.

Accede a la API desde Swagger:

http://localhost:5295/swagger
Endpoints principales
POST /api/auth/register → Registro de usuario

POST /api/auth/login → Login y obtención de token JWT

GET /api/productos → Lista de productos (autenticado)

POST /api/productos → Crear producto (rol Vendedor/Admin)

PUT /api/productos → Actualizar producto

DELETE /api/productos/{id} → Eliminar producto
Roles disponibles
Cliente

Vendedor

Administrador

Se aplican restricciones a cada uno en los controladores.


