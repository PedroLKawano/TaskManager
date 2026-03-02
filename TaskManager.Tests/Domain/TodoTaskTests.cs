using FluentAssertions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Tests.Domain;

public class TodoTaskTests
{
    [Fact]
    public void Should_Throw_When_Title_Is_Empty()
    {
        //Arrange
        var action = () => new TodoTask(title: string.Empty,
                                        Priority.Normal,
                                        "Description",
                                        new Category("Category name"));

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Throw_When_Title_Is_More_Than_50_Characters()
    {
        //Arrange
        var action = () => new TodoTask(title: "000000000000000000000000000000000000000000000000000",
                                        Priority.Normal,
                                        "Description",
                                        new Category("Category name"));

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Throw_When_Priority_Is_Urgent_And_Description_Is_Empty()
    {
        //Arrange
        var action = () => new TodoTask("Task name",
                                        Priority.Urgent,
                                        description: string.Empty,
                                        new Category("Category name"));

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Throw_When_Description_Is_More_Than_150_Characters()
    {
        //Arrange
        var action = () => new TodoTask("Task name",
                                        Priority.Normal,
                                        description: "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                                        new Category("Category name"));

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Throw_When_Category_Is_Null()
    {
        //Arrange
        var action = () => new TodoTask("Task name",
                                        Priority.Normal,
                                        "Description",
                                        category: null!);

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Set_Description_As_Empty_String_When_It_Is_Null()
    {
        //Arrange & Act
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                description: null!,
                                new Category("Category name"));

        //Assert
        task.Description.Should().Be(string.Empty);
    }

    [Fact]
    public void Should_Create_Valid_TodoTask()
    {
        //Arrange & Act
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        //Assert
        task.Id.Should().NotBe(Guid.Empty);
        task.Title.Should().Be("Task name");
        task.Priority.Should().Be(Priority.Normal);
        task.Description.Should().Be("Description");
        task.CategoryId.Should().NotBe(Guid.Empty);
        task.Category.Should().NotBe(null);
        task.CreationDate.Should().NotBe(DateTime.MinValue);
        task.Status.Should().Be(Status.Pending);
    }

    [Fact]
    public void Should_Throw_When_Trying_To_Start_With_Status_Not_Pending()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        task.Start();

        var action = () => task.Start();

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Start_Task_When_Status_Is_Pending()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        //Act
        task.Start();

        //Assert
        task.Status.Should().Be(Status.InProgress);
    }

    [Fact]
    public void Should_Throw_When_Trying_To_Complete_With_Status_Not_In_Progress()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        var action = () => task.Complete();

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Complete_Task_When_Status_Is_InProgress()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        task.Start();

        //Act
        task.Complete();

        //Assert
        task.ConclusionDate.Should().NotBe(null);
        task.Status.Should().Be(Status.Completed);
    }

    [Fact]
    public void Should_Throw_When_Trying_To_Cancel_With_Status_Completed()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        task.Start();
        task.Complete();

        var action = () => task.Cancel();

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Cancel_Task_When_Status_Is_Not_Completed()
    {
        //Arrange
        var task = new TodoTask("Task name",
                                Priority.Normal,
                                "Description",
                                new Category("Category name"));

        //Act
        task.Cancel();

        //Assert
        task.Status.Should().Be(Status.Canceled);
    }
}
