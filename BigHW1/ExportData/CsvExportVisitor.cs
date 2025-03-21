
using BigHW1.Entities;
using System.Globalization;

namespace BigHW1.ExportData
{
    public class CsvExportVisitor : Visitor
    {
        private List<string> _bankAccountLines = new List<string>();
        private List<string> _categoryLines = new List<string>();
        private List<string> _operationLines = new List<string>();

        public CsvExportVisitor(string filePath) : base(filePath)
        {
        }

        public override void Visit(BankAccount bankAccount)
        {
            // Формат: Id,Name,Balance
            _bankAccountLines.Add($"{bankAccount.Id},{bankAccount.Name},{bankAccount.Balance}");
        }

        public override void Visit(Category category)
        {
            // Формат: Id,Name,Type
            _categoryLines.Add($"{category.Id},{category.Name},{category.Type}");
        }

        public override void Visit(Operation operation)
        {
            // Форматируем дату в виде "dd.MM.yyyy HH:mm:ss"
            string dateStr = operation.Date.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            // Если Description равен null, заменяем на пустую строку
            string desc = operation.Description ?? "";
            // Формат: Id,Type,BankAccountId,Amount,Date,CategoryId,Description
            _operationLines.Add($"{operation.Id},{operation.Type},{operation.BankAccountId},{operation.Amount},{dateStr},{operation.CategoryId},{desc}");
        }

        public override void Save()
        {
            var lines = new List<string>();

            // Секция для BankAccounts
            lines.Add("BankAccounts");          // Название секции
            lines.Add("Id,Name,Balance");         // Заголовок секции
            lines.AddRange(_bankAccountLines);
            lines.Add("");                        // Пустая строка-разделитель

            // Секция для Categories
            lines.Add("Categories");
            lines.Add("Id,Name,Type");
            lines.AddRange(_categoryLines);
            lines.Add("");

            // Секция для Operations
            lines.Add("Operations");
            lines.Add("Id,Type,BankAccountId,Amount,Date,CategoryId,Description");
            lines.AddRange(_operationLines);

            File.WriteAllLines(_filePath, lines);
            Console.WriteLine($"CSV экспорт завершён. Файл записан по пути: {_filePath}");
        }
    }
}

