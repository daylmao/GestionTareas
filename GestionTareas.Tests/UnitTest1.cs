using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Enum;
using Moq;
using GestionTareas.Controllers;
using GestionTareas.Core.Application.Service;
using GestionTareas.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Tests
{
    public class TareaControllerTests
    {
        private readonly Mock<ITareaService> _taskServiceMock;

        public TareaControllerTests()
        {
            _taskServiceMock = new Mock<ITareaService>();
        }

        [Fact]
        public async Task CreateTarea_ShouldReturnSuccess()
        {
            // Arrange
            var createTarea = new CreateTareaDTO
            {
                Description = "Completar tarea de inglés",
                DueDate = new DateTime(2025, 10, 20),
                Status = Status.Pendiente,  
                AdditionalData = 2
            };

            var answerResult = Result<TareaDTO>.Success(new TareaDTO
            {
                Description = createTarea.Description,
                DueDate = createTarea.DueDate,
                Status = createTarea.Status,
                AdditionalData = createTarea.AdditionalData
            }, "200");

            _taskServiceMock.Setup(x => x.CreateAsync(createTarea))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.CreateTarea(createTarea);

            // Assert
            Assert.NotNull(result);
            
        }

        [Fact]
        public async Task CreateHighPriorityTarea_ShouldReturnSuccess()
        {
            // Arrange
            var description = "tarea de alta prioridad";

            var answerResult = Result<TareaDTO>.Success(new TareaDTO
            {
                Description = description,
                DueDate = new DateTime(2025, 07, 7),
                Status = Status.Pendiente,  
                AdditionalData = 1
            }, "200");

            _taskServiceMock.Setup(x => x.CreateHighPriority(description))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.CreateHighPriorityTarea(description);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateLowPriorityTarea_ShouldReturnSuccess()
        {
            // Arrange
            var description = "tarea de baja prioridad";

            var answerResult = Result<TareaDTO>.Success(new TareaDTO
            {
                Description = description,
                DueDate = new DateTime(2025, 09, 19),
                Status = Status.Pendiente, 
                AdditionalData = 2
            }, "200");

            _taskServiceMock.Setup(x => x.CreateLowPriority(description))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.CreateLowPriorityTarea(description);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess()
        {
            // Arrange
            var answerResult = Result<IEnumerable<TareaDTO>>.Success(new List<TareaDTO>
            {
                new TareaDTO
                {
                    Description = "tarea 1",
                    DueDate = new DateTime(2025, 05, 21),
                    Status = Status.Pendiente,  
                    AdditionalData = 3
                },
                new TareaDTO
                {
                    Description = "tarea 2",
                    DueDate = new DateTime(2025, 06, 15),
                    Status = Status.Completado, 
                    AdditionalData = 1
                }
            }, "200");

            _taskServiceMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnSuccess()
        {
            // Arrange
            int id = 123;
            var tareaDTO = new TareaDTO
            {
                Description = "tarea obtenida",
                DueDate = new DateTime(2025, 06, 23),
                Status = Status.Pendiente,  
                AdditionalData = 3
            };

            var answerResult = Result<TareaDTO>.Success(tareaDTO, "200");

            _taskServiceMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task FilterByStatus_ShouldReturnSuccess()
        {
            // Arrange
            Status status = Status.Pendiente;

            var answerResult = Result<IEnumerable<TareaDTO>>.Success(new List<TareaDTO>
            {
                new TareaDTO
                {
                    Description = "tarea filtrada",
                    DueDate = new DateTime(2025, 06, 22),
                    Status = Status.Pendiente,
                    AdditionalData = 1
                }
            }, "200");

            _taskServiceMock.Setup(x => x.FilterByStatus(status))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.FilterByStatusAsync(status);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);  
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccess_WithResultString()
        {
            // Arrange
            int id = 1;  

            var answerResult = Result<string>.Success("Tarea eliminada con éxito", "200");

            _taskServiceMock.Setup(x => x.DeleteAsync(id))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.DeleteAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result); 
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess()
        {
            // Arrange
            int id = 1;  
            var updateTarea = new UpdateTareaDTO
            {
                Description = "Tarea actualizada",
                DueDate = new DateTime(2025, 07, 01),
                Status = Status.Completado,  
                AdditionalData = 3
            };

            var answerResult = Result<TareaDTO>.Success(new TareaDTO
            {
                Description = updateTarea.Description,
                DueDate = updateTarea.DueDate,
                Status = updateTarea.Status,
                AdditionalData = updateTarea.AdditionalData
            }, "200");

            
            _taskServiceMock.Setup(x => x.UpdateAsync(id, updateTarea))
                .ReturnsAsync(answerResult);

            var tareaController = new TareaController(_taskServiceMock.Object);

            // Act
            var result = await tareaController.UpdateAsync(id, updateTarea);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result); 
        }


    }
}
