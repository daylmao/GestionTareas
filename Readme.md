
```markdown
# Arquitectura del Proyecto

El proyecto sigue una **arquitectura Onion**, que promueve la separación de responsabilidades a través de varias capas, lo que facilita la escalabilidad, mantenimiento y prueba del sistema.

## **Capa de API (Interfaz de Usuario)**

Esta capa es responsable de interactuar con el cliente, recibiendo las solicitudes HTTP, validando los datos y comunicándose con la capa de aplicación para realizar las operaciones solicitadas. 

### Principales responsabilidades:
- Exposición de los endpoints HTTP.
- Validación de datos de entrada.
- Manejo de errores y respuestas estructuradas.
- Seguridad y autorización en las solicitudes.

---

## **Capa de Aplicación (Lógica de Negocio)**

La capa de aplicación contiene la lógica de negocio y los servicios que gestionan las operaciones del sistema. Aquí es donde se encuentran los servicios que gestionan las tareas y realizan la lógica necesaria.

### Principales responsabilidades:
- **Servicios de aplicación**: Implementación de la lógica de negocio.
- **DTOs**: Transformación de datos entre las diferentes capas (por ejemplo, `TareaDTO`, `CreateTareaDTO`).
- **Validación y procesamiento**: Verificación y manipulación de los datos antes de persistirlos.

---

## **Capa de Dominio (Modelo del Negocio)**

En esta capa se definen los objetos de dominio, que son las entidades principales del sistema y representan conceptos fundamentales del negocio.

### Principales responsabilidades:
- **Entidades**: Modelos de datos como `Tarea`.
- **Enumeraciones**: Valores predefinidos para estados de las tareas, como `Pendiente`, `Completado`, `EnProgreso`.

---

## **Capa de Persistencia (Acceso a Datos)**

Esta capa maneja la interacción con la base de datos, donde se almacenan y recuperan los datos. Gestiona el acceso y la persistencia de las entidades del dominio.

### Principales responsabilidades:
- **Repositorios**: Métodos para interactuar con la base de datos.
- **Acceso a la base de datos**: Uso de tecnologías como ADO.NET o Entity Framework para acceder a los datos.

---

# Documentación de la API de Tareas

La siguiente sección describe los endpoints disponibles en la API para gestionar las tareas. Cada endpoint está diseñado para interactuar con el sistema de tareas y realizar operaciones como crear, obtener, actualizar, filtrar y eliminar tareas.

---

## **1. Crear Tarea**
**POST** `/api/tarea`

Crea una nueva tarea en el sistema.

### Parámetros:
- **Cuerpo de la solicitud**: Un objeto JSON que contiene los detalles de la tarea a crear.
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

### Respuesta:
- **200 OK**: La tarea fue creada exitosamente.
- **404 Not Found**: Si ocurrió un error inesperado durante la creación.

---

## **2. Obtener Todas las Tareas**
**GET** `/api/tarea`

Recupera todas las tareas existentes en el sistema.

### Respuesta:
- **200 OK**: Una lista de todas las tareas almacenadas.

---

## **3. Filtrar Tareas por Estado**
**GET** `/api/tarea/{status}`

Recupera todas las tareas con un estado específico.

### Parámetros:
- **status** (enum): El estado por el cual filtrar las tareas, puede ser `Pendiente`, `Completado` o `EnProgreso`.

### Respuesta:
- **200 OK**: Una lista de tareas con el estado solicitado.
- **400 Bad Request**: Si el estado proporcionado no es válido.

---

## **4. Actualizar Tarea**
**PUT** `/api/tarea/{id}`

Actualiza los detalles de una tarea existente en el sistema.

### Parámetros:
- **id** (int): El ID de la tarea a actualizar.

### Cuerpo de la solicitud:
```json
{
  "description": "Nueva descripción",
  "dueDate": "2025-02-01",
  "status": "Completado",
  "additionalData": 2
}
```

**Nota**: En este caso, la prioridad de la tarea se establece a 2 (prioridad media).

### Respuesta:
- **200 OK**: La tarea fue actualizada correctamente.
- **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.

---

## **5. Eliminar Tarea**
**DELETE** `/api/tarea/{id}`

Elimina una tarea específica por su ID.

### Parámetros:
- **id** (int): El ID de la tarea a eliminar.

### Respuesta:
- **200 OK**: La tarea fue eliminada exitosamente.
- **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.
