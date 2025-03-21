
using BigHW1.Entities;
using BigHW1.ExportData;

namespace BigHW1.Commands.ExportCommands
{
    public class JsonExportCommand : ICommand
    {
        JsonExportVisitor visitor;
        string _filePath;
        private List<BankAccount> _bankAccounts;
        private List<Category> _categories;
        private List<Operation> _operations;

        public JsonExportCommand(string filePath, List<BankAccount> bankAccounts, List<Category> categories, List<Operation> operations)
        {
            _filePath = filePath;
            _bankAccounts = bankAccounts;
            _categories = categories;
            _operations = operations;
        }

        public void Execute()
        {
            try
            {
                visitor = new JsonExportVisitor(_filePath);

                foreach (var account in _bankAccounts)
                {
                    account.Accept(visitor);
                }
                foreach (var category in _categories)
                {
                    category.Accept(visitor);
                }
                foreach (var operation in _operations)
                {
                    operation.Accept(visitor);
                }
                visitor.Save();
                Console.WriteLine("Экспорт в JSON выполнен.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при экспорте в JSON: {ex.Message}");
            }
        }
    }
}
