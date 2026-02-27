using FluentAssertions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Tests.Domain;

public class CategoryTests
{
    [Fact]
    public void Should_Throw_When_Name_Is_Empty()
    {
        //Arrange
        var action = () => new Category("");

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Throw_When_Name_Is_More_Then_50_Characters()
    {
        //Arrange
        var action = () => new Category("000000000000000000000000000000000000000000000000000");

        //Act & Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Create_Valid_Category()
    {
        //Arrange
        var category = new Category("Category name");

        //Act & Assert
        category.Name.Should().Be("Category name");
        category.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Update_Name_When_Valid()
    {
        //Arrange
        var category = new Category("Category name");

        //Act
        category.UpdateName("New name");

        //Assert
        category.Name.Should().Be("New name");
    }

    [Fact]
    public void Should_Throw_When_Description_Is_More_Then_150_Characters()
    {
        //Arrange
        var category = new Category("Category name");

        //Act
        var action = () => category.SetDescription("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

        //Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Should_Set_Valid_Description()
    {
        //Arrange
        var category = new Category("Category name");

        //Act
        category.SetDescription("Test description.");

        //Assert
        category.Description.Should().Be("Test description.");
    }
}
