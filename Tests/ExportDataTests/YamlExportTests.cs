
using BigHW1.Entities;
using BigHW1.ExportData;
using BigHW1.Facades;

namespace BigHW1.Tests.ExportDataTests
{
    public class YamlExportVisitorTests
    {
        private string _testFilePath;

        public YamlExportVisitorTests()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Directory.GetParent(currentDir)?.Parent?.Parent?.FullName
                             ?? currentDir;
            string filePath = Path.Combine(projectDir, "TestData");
            _testFilePath = Path.Combine(filePath, "export.yaml");
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
            var exportVisitor = new YamlExportVisitor(_testFilePath);
            exportVisitor.Visit(account1);
            exportVisitor.Visit(account2);
            exportVisitor.Visit(category1);
            exportVisitor.Visit(category2);
            exportVisitor.Visit(operation1);
            exportVisitor.Visit(operation2);
            exportVisitor.Save();

            // Assert
            var fileContent = File.ReadAllText(_testFilePath);

            Assert.Contains("BankAccounts", fileContent);
            Assert.Contains("Id: " + account1.Id, fileContent);
            Assert.Contains("Name: Account1", fileContent);
            Assert.Contains("Balance: 1500", fileContent);

            Assert.Contains("Categories", fileContent);
            Assert.Contains("Name: Salary", fileContent);
            Assert.Contains("Type: Income", fileContent);

            Assert.Contains("Operations", fileContent);
            Assert.Contains("Type: Income", fileContent);
            Assert.Contains("Amount: 2000", fileContent);
            Assert.Contains("Description: Salary for last month", fileContent);
        }

        [Fact]
        public void Export_EmptyData_ShouldExportEmptyYAML()
        {

            // Act
            var exportVisitor = new YamlExportVisitor(_testFilePath);
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
            var exportVisitor = new YamlExportVisitor(_testFilePath);
            exportVisitor.Visit(account1);
            exportVisitor.Visit(category1);
            exportVisitor.Visit(operation1);
            exportVisitor.Save();

            // Assert

            var fileContent = File.ReadAllText(_testFilePath);

            Assert.Contains("Name: Account&1", fileContent);
            Assert.Contains("Name: Salary*Special", fileContent);
            Assert.Contains("Description: Salary for *last month&", fileContent);
        }
        public void Export_InvalidFilePath_ThrowsFileNotFoundException()
        {
            // Arrange
            string invalidFilePath = Path.Combine(Directory.GetCurrentDirectory(), "NonExistentDirectory", "export_test.yaml");
            var exception = Assert.Throws<FileNotFoundException>(() => new YamlExportVisitor(invalidFilePath));
            Assert.Equal("Файл для записи не найден", exception.Message);
        }
    }
}
