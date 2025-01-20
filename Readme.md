
```markdown
# Arquitectura del Proyecto

El proyecto sigue una **arquitectura Onion**, que promueve la separación de responsabilidades a través de varias capas. Esta estructura facilita la escalabilidad, mantenimiento y prueba del sistema.

---

## 🏢 **Capa de API (Interfaz de Usuario)**

Esta capa se encarga de interactuar con los clientes. Recibe solicitudes HTTP, valida los datos y se comunica con la capa de aplicación para realizar las operaciones solicitadas.

### 📌 Responsabilidades principales:
- **Exposición de los endpoints HTTP**.
- **Validación de datos de entrada**.
- **Manejo de errores y respuestas estructuradas**.
- **Seguridad y autorización**.

---

## 💼 **Capa de Aplicación (Lógica de Negocio)**

Contiene la lógica de negocio y los servicios encargados de gestionar las operaciones del sistema, como la manipulación de las tareas.

### 📌 Responsabilidades principales:
- **Servicios de aplicación**: Implementación de la lógica de negocio.
- **DTOs**: Transformación de datos entre las diferentes capas (por ejemplo, `TareaDTO`, `CreateTareaDTO`).
- **Validación y procesamiento**: Verificación y manipulación de los datos antes de persistirlos.

---

## 🏗️ **Capa de Dominio (Modelo del Negocio)**

Define los objetos de dominio, que representan conceptos fundamentales del negocio. Esta capa es el corazón del sistema.

### 📌 Responsabilidades principales:
- **Entidades**: Modelos de datos como `Tarea`.
- **Enumeraciones**: Valores predefinidos para estados de las tareas, como `Pendiente`, `Completado`, `EnProgreso`.

---

## 💾 **Capa de Persistencia (Acceso a Datos)**

Gestiona la interacción con la base de datos y la persistencia de las entidades del dominio. Aquí se implementan los repositorios que permiten el acceso a los datos.

### 📌 Responsabilidades principales:
- **Repositorios**: Métodos para interactuar con la base de datos.
- **Acceso a la base de datos**: Uso de tecnologías como ADO.NET o Entity Framework para acceder a los datos.

---

# 📚 **Documentación de la API de Tareas**

A continuación, se describen los **endpoints** disponibles para gestionar las tareas en el sistema.

---

## 1️⃣ **Crear Tarea**  
**POST** `/api/tarea`  
Crea una nueva tarea en el sistema.

### 📝 **Parámetros**:
- **Cuerpo de la solicitud**: Un objeto JSON con los detalles de la tarea.
  ```json
  {
    "description": "Descripción de la tarea",
    "dueDate": "2025-01-25",
    "status": "Pendiente",
    "additionalData": 1
  }
  ```
  **Nota**: El campo `additionalData` representa la prioridad de la tarea:
  - **1**: Alta prioridad.
  - **2**: Prioridad media.
  - **3**: Baja prioridad.

### 📬 **Respuesta**:
- **200 OK**: La tarea fue creada exitosamente.
- **404 Not Found**: Si ocurrió un error inesperado durante la creación.

---

## 2️⃣ **Obtener Todas las Tareas**  
**GET** `/api/tarea`  
Recupera todas las tareas existentes en el sistema.

### 📬 **Respuesta**:
- **200 OK**: Lista de todas las tareas almacenadas.

---

## 3️⃣ **Filtrar Tareas por Estado**  
**GET** `/api/tarea/{status}`  
Recupera todas las tareas filtradas por su estado.

### 📝 **Parámetros**:
- **status** (enum): El estado de la tarea (puede ser `Pendiente`, `Completado`, `EnProgreso`).

### 📬 **Respuesta**:
- **200 OK**: Lista de tareas con el estado solicitado.
- **400 Bad Request**: Si el estado proporcionado no es válido.

---

## 4️⃣ **Actualizar Tarea**  
**PUT** `/api/tarea/{id}`  
Actualiza una tarea existente en el sistema.

### 📝 **Parámetros**:
- **id** (int): El ID de la tarea a actualizar.

### 📝 **Cuerpo de la solicitud**:
```json
{
  "description": "Nueva descripción",
  "dueDate": "2025-02-01",
  "status": "Completado",
  "additionalData": 2
}
```
**Nota**: En este caso, la prioridad de la tarea se establece a 2 (prioridad media).

### 📬 **Respuesta**:
- **200 OK**: La tarea fue actualizada correctamente.
- **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.

---

## 5️⃣ **Eliminar Tarea**  
**DELETE** `/api/tarea/{id}`  
Elimina una tarea específica por su ID.

### 📝 **Parámetros**:
- **id** (int): El ID de la tarea a eliminar.

### 📬 **Respuesta**:
- **200 OK**: La tarea fue eliminada exitosamente.
- **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.
