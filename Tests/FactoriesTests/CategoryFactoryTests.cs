using System;
using Xunit;
using BigHW1.Factories;
using BigHW1.Entities;

namespace BigHW1.Tests.FactoriesTests
{
    public class CategoryFactoryTests
    {
        [Theory]
        [InlineData("Salary", "Income")]
        [InlineData("Groceries", "Expense")]
        public void CreateCategory_ValidData_ReturnsCategory(string name, string type)
        {
            // Arrange
            var factory = new CategoryFactory();

            // Act
            var category = factory.CreateCategory(name, type);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.NotEqual(Guid.Empty, category.Id);
        }

        [Fact]
        public void CreateCategory_InvalidType_ThrowsArgumentException()
        {
            // Arrange
            var factory = new CategoryFactory();
            var invalidType = "SomethingElse";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                factory.CreateCategory("TestCategory", invalidType);
            });
        }
    }
}
