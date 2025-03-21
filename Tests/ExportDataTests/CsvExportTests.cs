using System;
using System.IO;
using Xunit;
using BigHW1.Entities;
using BigHW1.ExportData;
using Newtonsoft.Json;
using BigHW1.Facades;

namespace BigHW1.Tests.ExportDataTests
{
    public class CsvExportVisitorTests
    {
        private string _testFilePath;

        public CsvExportVisitorTests()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Directory.GetParent(currentDir)?.Parent?.Parent?.FullName
                             ?? currentDir;
            string filePath = Path.Combine(projectDir, "TestData");
            _testFilePath = Path.Combine(filePath, "export.csv");
        }

        [Fact]
        public void Export_ValidData_ShouldExportCorrectly()
        {
            // Arrange

            var account1 = new BankAccount("Account1", 1500);
            var account2 = new BankAccount("Account2", 500);


            var category1 = new Category("Salary", "Income");
            var category2 = new Category("Groceries", "Expense");


            var operation1 = new Operation("Income", account1.Id, 2000, DateTime.Now, category1.Id, "Salary for last month");
            var operation2 = new Operation("Expense", account1.Id, 500, DateTime.Now, category2.Id, "Grocery shopping");


            // Act
            var exportVisitor = new JsonExportVisitor(_testFilePath);
            exportVisitor.Visit(account1);
            exportVisitor.Visit(account2);
            exportVisitor.Visit(category1);
            exportVisitor.Visit(category2);
            exportVisitor.Visit(operation1);
            exportVisitor.Visit(operation2);
            exportVisitor.Save();

            // Assert

            var fileContent = File.ReadAllText(_testFilePath);

            Assert.Contains(account1.Id.ToString(), fileContent);
            Assert.Contains(account1.Name, fileContent);
            Assert.Contains(account1.Balance.ToString(), fileContent);

            Assert.Contains(category1.Name, fileContent);
            Assert.Contains(category1.Type, fileContent);

            Assert.Contains(operation1.Description, fileContent);
            Assert.Contains(operation1.Amount.ToString(), fileContent);
        }

        [Fact]
        public void Export_EmptyData_ShouldExportEmptyJSON()
        {
            // Act
            var exportVisitor = new JsonExportVisitor(_testFilePath);
            exportVisitor.Save();

            // Assert

            var fileContent = File.ReadAllText(_testFilePath);
            Assert.Contains("BankAccounts", fileContent);
            Assert.Contains("Categories", fileContent);
            Assert.Contains("Operations", fileContent);
        }

        [Fact]
        public void Export_DataWithSpecialCharacters_ShouldExportCorrectly()
        {
            // Arrange
            var account1 = new BankAccount("Account&1", 1500);

            var category1 = new Category("Salary*Special", "Income");

            var operation1 = new Operation("Income", account1.Id, 2000, DateTime.Now, category1.Id, "Salary for *last month&");

            // Act
            var exportVisitor = new JsonExportVisitor(_testFilePath);
            exportVisitor.Visit(account1);
            exportVisitor.Visit(category1);
            exportVisitor.Visit(operation1);
            exportVisitor.Save();

            // Assert

            var fileContent = File.ReadAllText(_testFilePath);

            Assert.Contains("Account&1", fileContent);
            Assert.Contains("Salary*Special", fileContent);
            Assert.Contains("Salary for *last month&", fileContent);
        }

        [Fact]
        public void Export_InvalidFilePath_ThrowsFileNotFoundException()
        {
            // Arrange
            string invalidFilePath = Path.Combine(Directory.GetCurrentDirectory(), "NonExistentDirectory", "export_test.json");

            // Act & Assert
            var exception = Assert.Throws<FileNotFoundException>(() => new JsonExportVisitor(invalidFilePath));
            Assert.Equal("Файл для записи не найден", exception.Message);
        }
    }
}
