
using BigHW1.Entities;
using YamlDotNet.Serialization;

namespace BigHW1.ExportData
{
    public class YamlExportVisitor : Visitor
    {
        private List<BankAccount> _bankAccounts = new List<BankAccount>();
        private List<Category> _categories = new List<Category>();
        private List<Operation> _operations = new List<Operation>();

        public YamlExportVisitor(string filePath): base(filePath) { }

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

            var serializer = new Serializer();
            string yaml = serializer.Serialize(data);
            File.WriteAllText(_filePath, yaml);
        }
    }
}
