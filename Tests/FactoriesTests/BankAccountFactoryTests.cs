using BigHW1.Factories;

namespace BigHW1.Tests.FactoriesTests
{
    public class BankAccountFactoryTests
    {
        [Fact]
        public void CreateAccount_ValidData_ReturnsBankAccount()
        {
            // Arrange
            var factory = new BankAccountFactory();
            var expectedName = "TestAccount";
            var expectedBalance = 1000m;

            // Act
            var account = factory.CreateAccount(expectedName, expectedBalance);

            // Assert
            Assert.NotNull(account);
            Assert.Equal(expectedName, account.Name);
            Assert.Equal(expectedBalance, account.Balance);
            Assert.NotEqual(Guid.Empty, account.Id); // Проверяем, что Id не пустой
        }

        [Fact]
        public void CreateAccount_NegativeBalance_ThrowsArgumentException()
        {
            // Arrange
            var factory = new BankAccountFactory();
            var negativeBalance = -100m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                factory.CreateAccount("TestAccount", negativeBalance);
            });
        }
    }
}
