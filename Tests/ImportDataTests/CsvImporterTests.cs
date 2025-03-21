
using BigHW1.ImportData;

namespace BigHW1.Tests.ImportDataTests
{
    public class CsvImporterTests
    {
        [Fact]
        public void Import_ValidCsvFile_ParsesAllData()
        {
            // Arrange
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Directory.GetParent(currentDir)?.Parent?.Parent?.FullName
                             ?? currentDir;
            string filePath = Path.Combine(projectDir, "TestData");
            filePath = Path.Combine(filePath, "import.csv");
            var importer = new CsvImporter();

            // Act
            importer.Import(filePath);

            // Assert
            // 1) Проверяем количество сущностей
            Assert.Equal(2, importer._bankAccounts.Count);
            Assert.Equal(2, importer._categories.Count);
            Assert.Equal(2, importer._operations.Count);

            // 2) Проверяем первый BankAccount
            var acc1 = importer._bankAccounts[0];
            Assert.Equal("Account1", acc1.Name);
            Assert.Equal(1500m, acc1.Balance);
            Assert.Equal(Guid.Parse("1bacffee-f56c-49b8-91fd-d89667763df6"), acc1.Id);

            // 3) Проверяем второй BankAccount
            var acc2 = importer._bankAccounts[1];
            Assert.Equal("Account2", acc2.Name);
            Assert.Equal(500m, acc2.Balance);
            Assert.Equal(Guid.Parse("212a9fc8-7295-476f-aa3c-25a0a9f4dbe5"), acc2.Id);

            // 4) Проверяем первую Category
            var cat1 = importer._categories[0];
            Assert.Equal("Salary", cat1.Name);
            Assert.Equal("Income", cat1.Type);
            Assert.Equal(Guid.Parse("be2c1122-887e-4482-a535-cf5ee5ca81e4"), cat1.Id);

            // 5) Проверяем вторую Category
            var cat2 = importer._categories[1];
            Assert.Equal("Groceries", cat2.Name);
            Assert.Equal("Expense", cat2.Type);
            Assert.Equal(Guid.Parse("1f541e7a-ae90-4d4c-9f89-09a6bc409101"), cat2.Id);

            // 6) Проверяем первую Operation
            var op1 = importer._operations[0];
            Assert.Equal("Income", op1.Type);
            Assert.Equal(Guid.Parse("1bacffee-f56c-49b8-91fd-d89667763df6"), op1.BankAccountId);
            Assert.Equal(2000m, op1.Amount);
            Assert.Equal(Guid.Parse("be2c1122-887e-4482-a535-cf5ee5ca81e4"), op1.CategoryId);
            Assert.Equal("Salary for last month", op1.Description);
            var expectedDate1 = new DateTime(2025, 3, 15, 16, 7, 22);
            Assert.Equal(expectedDate1, op1.Date);

            // 7) Проверяем вторую Operation
            var op2 = importer._operations[1];
            Assert.Equal("Expense", op2.Type);
            Assert.Equal(Guid.Parse("1bacffee-f56c-49b8-91fd-d89667763df6"), op2.BankAccountId);
            Assert.Equal(500m, op2.Amount);
            Assert.Equal(Guid.Parse("1f541e7a-ae90-4d4c-9f89-09a6bc409101"), op2.CategoryId);
            Assert.Equal("Grocery shopping", op2.Description);
            var expectedDate2 = new DateTime(2025, 3, 17, 16, 7, 22);
            Assert.Equal(expectedDate2, op2.Date);
        }
        [Fact]
        public void Import_ThrowFileNotFoundException()
        {
            // Arrange
            var importer = new CsvImporter(); 

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
            {
                importer.Import("");
            });

        }
    }
}
