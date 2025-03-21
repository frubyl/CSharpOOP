using System;
using Xunit;
using BigHW1.Entities;
using BigHW1.Facades;

namespace BigHW1.Tests.FacadesTests
{
    public class CategoryFacadeTests
    {
        [Fact]
        public void AddCategory_ValidCategory_ShouldAddToList()
        {
            // Arrange
            var facade = new CategoryFacade();
            var category = new Category("Salary", "Income");

            // Act
            facade.AddCategory(category);

            // Assert
            Assert.Single(facade._categories);
            Assert.Equal("Salary", facade._categories[0].Name);
        }

        [Fact]
        public void EditCategory_ValidId_ShouldUpdateCategoryName()
        {
            // Arrange
            var facade = new CategoryFacade();
            var category = new Category("Salary", "Income");
            facade.AddCategory(category);

            // Act
            facade.EditCategory(category.Id, "UpdatedCategory");

            // Assert
            Assert.Equal("UpdatedCategory", facade._categories[0].Name);
        }

        [Fact]
        public void DeleteCategory_ValidId_ShouldRemoveCategory()
        {
            // Arrange
            var facade = new CategoryFacade();
            var category = new Category("Salary", "Income");
            facade.AddCategory(category);

            // Act
            facade.DeleteCategory(category.Id);

            // Assert
            Assert.Empty(facade._categories);
        }

        [Fact]
        public void EditCategory_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new CategoryFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.EditCategory(invalidId, "NonExistentCategory"));
        }

        [Fact]
        public void DeleteCategory_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new CategoryFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.DeleteCategory(invalidId));
        }
    }
}
