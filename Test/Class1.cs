namespace Test;

[Fact]
public void CreateTask()
{
    //arrange
    CreateTaskDto createTaskDto = new CreateTaskDto(
        "Description about task 1", 
        new DateOnly(2025, 3, 15), 
        Status.Pending,
        10 );

    var taskDtos = new TaskDtos 
    (
        Id:Guid.NewGuid(),
        Description: createTaskDto.DescriptionAboutTask,
        DuaDate: createTaskDto.DuaDate,
        Status: createTaskDto.StatusTask,
        AdditionalData: createTaskDto.AdditionalData
    );

    var expectedResult = ResultT<TaskDtos>.Success(taskDtos);

    taskServiceMock.Setup(x => x.CreateAsync(createTaskDto, It.IsAny<CancellationToken>()))
        .ReturnsAsync(expectedResult);
    //Act
    var result =  taskServiceMock.Object.CreateAsync(createTaskDto, CancellationToken.None);

    // Assert
    //Assert.NotNull(result);
    Assert.True(result.IsCompleted);
}