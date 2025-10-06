# Tienda Papelería (Blazor Server)

Proyecto base de una tienda en línea de papelería construido con Blazor Server (.NET 9). Incluye autenticación con ASP.NET Core Identity, componentes reutilizables y servicios en memoria para el catálogo y el carrito de compras.

## Características principales

- Landing page moderna y responsive con Bootstrap 5.
- Registro e inicio de sesión con Identity y almacenamiento en memoria.
- Dashboard para usuarios autenticados con resumen de perfil y carrito.
- Catálogo filtrable por categorías y búsqueda.
- Carrito de compras con operaciones de agregar, quitar y limpiar.
- Componentes reutilizables para tarjetas de producto y elementos del carrito.

## Estructura del proyecto

```
TiendaPapeleria.sln
TiendaPapeleria/
  Components/
  Data/
  Models/
  Pages/
  Services/
  Shared/
  wwwroot/
```

## Próximos pasos sugeridos

- Conectar el contexto de datos a una base de datos real (SQL Server, PostgreSQL, etc.).
- Reemplazar los servicios en memoria por repositorios/patrones de dominio.
- Implementar flujos de pago y gestión de pedidos.
- Agregar pruebas automatizadas.
