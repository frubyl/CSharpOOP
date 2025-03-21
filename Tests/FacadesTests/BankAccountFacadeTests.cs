using System;
using Xunit;
using BigHW1.Entities;
using BigHW1.Facades;

namespace BigHW1.Tests.FacadesTests
{
    public class BankAccountFacadeTests
    {
        [Fact]
        public void AddBankAccount_ValidAccount_ShouldAddToList()
        {
            // Arrange
            var facade = new BankAccountFacade();
            var account = new BankAccount("Account1", 1000);

            // Act
            facade.AddBankAccount(account);

            // Assert
            Assert.Single(facade._bankAccounts);
            Assert.Equal("Account1", facade._bankAccounts[0].Name);
        }

        [Fact]
        public void EditBankAccount_ValidId_ShouldUpdateAccountName()
        {
            // Arrange
            var facade = new BankAccountFacade();
            var account = new BankAccount("Account1", 1000);
            facade.AddBankAccount(account);

            // Act
            facade.EditBankAccount(account.Id, "UpdatedAccount");

            // Assert
            Assert.Equal("UpdatedAccount", facade._bankAccounts[0].Name);
        }

        [Fact]
        public void DeleteBankAccount_ValidId_ShouldRemoveAccount()
        {
            // Arrange
            var facade = new BankAccountFacade();
            var account = new BankAccount("Account1", 1000);
            facade.AddBankAccount(account);

            // Act
            facade.DeleteBankAccount(account.Id);

            // Assert
            Assert.Empty(facade._bankAccounts);
        }

        [Fact]
        public void EditBankAccount_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new BankAccountFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.EditBankAccount(invalidId, "NonExistentAccount"));
        }

        [Fact]
        public void DeleteBankAccount_InvalidId_ShouldThrowException()
        {
            // Arrange
            var facade = new BankAccountFacade();
            var invalidId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                facade.DeleteBankAccount(invalidId));
        }
    }
}
