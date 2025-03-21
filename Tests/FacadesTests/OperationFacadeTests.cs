using System;
using Xunit;
using BigHW1.Entities;
using BigHW1.Facades;

namespace BigHW1.Tests.FacadesTests
{
    public class OperationFacadeTests
    {
        [Fact]
        public void AddOperation_ValidOperation_ShouldAddToList()
        {
            // Arrange
            var facade = new OperationFacade();
            var operation = new Operation("Income", Guid.NewGuid(), 1000, DateTime.Now, Guid.NewGuid(), "Salary for last month");

            // Act
            facade.AddOperation(operation);

            // Assert
            Assert.Single(facade._operations);
            Assert.Equal("Salary for last month", facade._operations[0].Description);
        }

        [Fact]
        public void EditOperation_ValidId_ShouldUpdateDescription()
        {
            // Arrange
            var facade = new OperationFacade();
            var operation = new Operation("Income", Guid.NewGuid(), 1000, DateTime.Now, Guid.NewGuid(), "Salary for last month");
            facade.AddOperation(operation);

            // Act
            facade.EditOperation(operation.Id, "Updated Description");

            // Assert
            Assert.Equal("Updated Description", facade._operations[0].Description);
        }

        [Fact]
        public void DeleteOperation_ValidId_ShouldRemoveOperation()
        {
            // Arrange
            var facade = new OperationFacade();
            var operation = new Operation("Income", Guid.NewGuid(), 1000, DateTime.Now, Guid.NewGuid(), "Salary for last month");
            facade.AddOperation(operation);

            // Act
            facade.DeleteOperation(operation.Id);

            // Assert
            Assert.Empty(facade._operations);
        }

        [Fact]
        public void EditOperation_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new OperationFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.EditOperation(invalidId, "NonExistentOperation"));
        }

        [Fact]
        public void DeleteOperation_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new OperationFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.DeleteOperation(invalidId));
        }
    }
}
