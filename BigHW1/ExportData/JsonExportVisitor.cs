using BigHW1.Entities;
using BigHW1.ExportData;
using Newtonsoft.Json;

namespace BigHW1.ExportData
{
    public class JsonExportVisitor : Visitor
    {
        private readonly List<BankAccount> _bankAccounts = new List<BankAccount>();
        private readonly List<Category> _categories = new List<Category>();
        private readonly List<Operation> _operations = new List<Operation>();
        public JsonExportVisitor(string filePath) : base(filePath) {  }

        public override void Visit(BankAccount bankAccount)
        {
            _bankAccounts.Add(bankAccount);
        }

        public override void Visit(Category category)
        {
            _categories.Add(category);
        }

        public override void Visit(Operation operation)
        {
            _operations.Add(operation);
        }

        public override void Save()
        {
            var data = new
            {
                BankAccounts = _bankAccounts,
                Categories = _categories,
                Operations = _operations
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}