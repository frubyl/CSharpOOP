
using BigHW1.Factories;

namespace BigHW1.Tests.FactoriesTests
{
    public class OperationFactoryTests
    {
        [Fact]
        public void CreateOperation_ValidData_ReturnsOperation()
        {
            // Arrange
            var factory = new OperationFactory();
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var amount = 500m;
            var type = "Income";
            var date = DateTime.Now;
            var description = "Test Operation";

            // Act
            var operation = factory.CreateOperation(accountId, categoryId, amount, type, date, description);

            // Assert
            Assert.NotNull(operation);
            Assert.Equal(accountId, operation.BankAccountId);
            Assert.Equal(categoryId, operation.CategoryId);
            Assert.Equal(amount, operation.Amount);
            Assert.Equal(type, operation.Type);
            Assert.Equal(description, operation.Description);
            Assert.Equal(date, operation.Date, precision: TimeSpan.FromSeconds(1));
            Assert.NotEqual(Guid.Empty, operation.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void CreateOperation_NonPositiveAmount_ThrowsArgumentException(decimal invalidAmount)
        {
            // Arrange
            var factory = new OperationFactory();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                factory.CreateOperation(
                    bankAccountId: Guid.NewGuid(),
                    categoryId: Guid.NewGuid(),
                    amount: invalidAmount,
                    type: "Income",
                    date: DateTime.Now,
                    description: "Invalid Amount"
                );
            });
        }

        [Fact]
        public void CreateOperation_InvalidType_ThrowsArgumentException()
        {
            // Arrange
            var factory = new OperationFactory();
            var invalidType = "SomethingElse";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                factory.CreateOperation(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    100m,
                    invalidType,
                    DateTime.Now,
                    "Invalid Type"
                );
            });
        }
    }
}
