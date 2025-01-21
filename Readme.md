
```markdown
# 🚀 Arquitectura del Proyecto

Este proyecto sigue una **arquitectura Onion**, que promueve la separación de responsabilidades. Esto facilita la escalabilidad, mantenimiento y prueba del sistema.

---

## 🖥️ API (Interfaz de Usuario)

Esta capa se encarga de interactuar con los clientes. Gestiona las solicitudes HTTP, valida los datos y se comunica con la capa de aplicación para realizar las operaciones solicitadas.

### Responsabilidades:
- 🌐 Exponer los endpoints HTTP.
- ✅ Validar los datos de entrada.
- ⚠️ Manejar errores y proporcionar respuestas estructuradas.
- 🔒 Gestionar la seguridad y autorización de las solicitudes.

---

## 💼 Capa de Aplicación (Lógica de Negocio)

Aquí reside la lógica de negocio, donde los servicios gestionan las operaciones principales del sistema. Incluye la gestión de tareas y la implementación de la lógica necesaria para procesarlas.

### Responsabilidades:
- 🛠️ Implementación de los servicios de negocio.
- 📦 Transformación de datos entre las capas utilizando DTOs (por ejemplo, `TareaDTO`, `CreateTareaDTO`).
- 🧹 Validación y procesamiento de los datos antes de almacenarlos.

---

## 🏢 Capa de Dominio (Modelo del Negocio)

En esta capa se definen las entidades que representan los conceptos fundamentales del negocio. Contiene los modelos de datos del sistema y las enumeraciones (por ejemplo, los estados de las tareas).

### Responsabilidades:
- 📝 Entidades como `Tarea`, que modelan los datos del negocio.
- 📊 Enumeraciones que definen valores preestablecidos, como los estados de las tareas: `Pendiente`, `Completado`, `EnProgreso`.

---

## 💾 Capa de Persistencia (Acceso a Datos)

Esta capa es responsable de interactuar con la base de datos para almacenar y recuperar las entidades del dominio. Se encarga del acceso y la persistencia de los datos.

### Responsabilidades:
- 📚 Repositorios que implementan métodos para acceder a la base de datos.
- 🏗️ Utilización de tecnologías como ADO.NET o Entity Framework para interactuar con los datos.

---

# 📋 Documentación de la API

A continuación se presentan los endpoints disponibles para gestionar las tareas, como crear, obtener, actualizar, filtrar y eliminar tareas.

---

## 1. ✍️ Crear Tarea  
**POST** `/api/tarea`  
Permite crear una nueva tarea en el sistema.

### Parámetros:
```json
{
  "description": "Descripción de la tarea",
  "dueDate": "2025-01-25",
  "status": "Pendiente",
  "additionalData": 1
}
```

**Nota**: El campo `additionalData` indica la prioridad de la tarea:
- **1**: Alta prioridad 🚨.
- **2**: Prioridad media ⚙️.
- **3**: Baja prioridad 🐢.

### Respuesta:
- **200 OK** ✅: La tarea se creó exitosamente.
- **404 Not Found** ❌: Hubo un error al crear la tarea.

---

## 2. 🔎 Obtener Tareas  
**GET** `/api/tarea`  
Recupera todas las tareas almacenadas en el sistema.

### Respuesta:
- **200 OK** ✅: Devuelve una lista de todas las tareas.

---

## 3. 🔄 Filtrar Tareas  
**GET** `/api/tarea/{status}`  
Recupera las tareas filtradas por su estado (puede ser `Pendiente`, `Completado`, o `EnProgreso`).

### Respuesta:
- **200 OK** ✅: Devuelve una lista de tareas con el estado solicitado.
- **400 Bad Request** ❌: El estado proporcionado no es válido.

---

## 4. ✏️ Actualizar Tarea  
**PUT** `/api/tarea/{id}`  
Actualiza los detalles de una tarea existente.

### Parámetros:
```json
{
  "description": "Nueva descripción",
  "dueDate": "2025-02-01",
  "status": "Completado",
  "additionalData": 2
}
```

**Nota**: El campo `additionalData` establece la prioridad de la tarea. En este ejemplo, la tarea tiene prioridad **2** (media).

### Respuesta:
- **200 OK** ✅: La tarea se actualizó correctamente.
- **404 Not Found** ❌: La tarea con el ID proporcionado no fue encontrada.

---

## 5. 🗑️ Eliminar Tarea  
**DELETE** `/api/tarea/{id}`  
Elimina una tarea específica por su ID.

### Respuesta:
- **200 OK** ✅: La tarea fue eliminada exitosamente.
- **404 Not Found** ❌: No se encontró la tarea con el ID proporcionado.
