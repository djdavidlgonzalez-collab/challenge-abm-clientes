# Challenge ABM Clientes – API REST

API REST desarrollada en **ASP.NET Core** para la gestión de clientes (ABM), como parte de un desafío técnico backend.

El proyecto implementa operaciones CRUD completas, búsqueda por nombre mediante **Stored Procedure**, validaciones de datos, manejo centralizado de errores, logging y documentación con Swagger.

---

## 🛠️ Tecnologías utilizadas

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Stored Procedures
- Swagger / OpenAPI
- Serilog (logging en archivo)

---

## 📌 Funcionalidades

La API permite:

- Obtener todos los clientes
- Obtener un cliente por ID
- Buscar clientes por nombre (coincidencia parcial)
- Crear un nuevo cliente
- Actualizar un cliente existente
- Eliminar un cliente

Todos los endpoints pueden probarse desde **Swagger UI**.

---

## ✅ Validaciones implementadas

- Campos obligatorios:
  - Nombre
  - Apellido
  - Razón Social
  - CUIT
  - Teléfono Celular
  - Email
  - Fecha de nacimiento

- Validaciones de formato:
  - Email válido
  - CUIT con formato correcto
  - Fecha de nacimiento válida

- Validaciones de negocio:
  - CUIT único (no se permite duplicado)

---

## 🔍 Stored Procedure

Para cumplir con el requerimiento del desafío, la búsqueda de clientes por nombre se realiza mediante un **Stored Procedure**:

- `dbo.sp_SearchClientes`

Este procedimiento recibe un parámetro `@Nombre` y devuelve los clientes cuya coincidencia sea parcial en nombre o apellido.

---

## ⚠️ Manejo de errores

- Manejo centralizado de excepciones mediante middleware
- Respuestas claras para:
  - Recursos no encontrados (404)
  - Errores de validación (400)
  - Errores inesperados (500)

---

## 📝 Logging

Los errores y eventos relevantes se registran utilizando **Serilog**, generando archivos diarios de log en la carpeta:


---

## 🗄️ Configuración de Base de Datos

Este proyecto no incluye credenciales de base de datos por motivos de seguridad.

### Pasos para ejecutar el proyecto:

# 1️) Crear la base de datos

Crear una base de datos SQL Server (local o en la nube).

-- =====================================================
-- CREACIÓN DE BASE DE DATOS
-- Challenge_ABM_Clientes
-- =====================================================

IF NOT EXISTS (
    SELECT name
    FROM sys.databases
    WHERE name = 'Challenge_ABM_Clientes'
)
BEGIN
    CREATE DATABASE Challenge_ABM_Clientes;
END
GO

-- =====================================================
-- USAR LA BASE DE DATOS
-- =====================================================
USE Challenge_ABM_Clientes;
GO


## 2) Crear la tabla `clientes`

CREATE TABLE dbo.clientes (
    id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    razon_social NVARCHAR(150) NOT NULL,
    cuit NVARCHAR(20) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    telefono_celular NVARCHAR(20) NOT NULL,
    email NVARCHAR(150) NOT NULL,

    CONSTRAINT PK_clientes PRIMARY KEY (id),
    CONSTRAINT UQ_clientes_cuit UNIQUE (cuit)
);
GO

### 3) Insertar datos de ejemplo

INSERT INTO dbo.clientes
(nombre, apellido, razon_social, cuit, fecha_nacimiento, telefono_celular, email)
VALUES
('Juan', 'Perez', 'JP Servicios SRL', '20-12345678-9', '1985-06-15', '1165874210', 'juan.perez@example.com'),
('Maria', 'Gomez', 'MG Soluciones', '27-23456789-0', '1990-09-21', '1165874221', 'maria.gomez@example.com'),
('Carlos', 'Lopez', 'CL Construcciones', '23-34567890-1', '1978-01-10', '1165874332', 'carlos.lopez@example.com'),
('Lucia', 'Martinez', 'LM Consultora', '27-45678901-2', '1992-03-05', '1165874443', 'lucia.martinez@example.com'),
('Diego', 'Fernandez', 'DF Diseño', '20-56789012-3', '1988-11-22', '1165874554', 'diego.fernandez@example.com');
GO

#### 4) Crear el Stored Procedure `sp_SearchClientes`

CREATE OR ALTER PROCEDURE dbo.sp_SearchClientes
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombre,
        apellido,
        razon_social,
        cuit,
        fecha_nacimiento,
        telefono_celular,
        email
    FROM dbo.clientes
    WHERE nombre LIKE '%' + @Nombre + '%'
       OR apellido LIKE '%' + @Nombre + '%';
END
GO

##### 5) Configurar la cadena de conexión

Configura el archivo appsettings.json en la raíz del proyecto con el siguiente contenido: 

    {
      "ConnectionStrings": {
        "DefaultConnection": "TU_CONNECTION_STRING_AQUI"
      }	
    }
        
## Autor David Luis Gonzalez