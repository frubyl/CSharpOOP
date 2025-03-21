using BigHW1.Entities;

namespace BigHW1.ImportData
{
    public abstract class DataImporter
    {
        public List<Category> _categories = new List<Category>();
        public List<BankAccount> _bankAccounts = new List<BankAccount>();
        public List<Operation> _operations = new List<Operation>();

        public void Import(string filePath)
        {
            ValidateFile(filePath);
            var data = ReadFile(filePath);
            ParseData(data);
        }
        public void ValidateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл для чтения не найден", filePath);
            }
        }
        protected virtual string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
        protected abstract void ParseData(string data);
    }
}
