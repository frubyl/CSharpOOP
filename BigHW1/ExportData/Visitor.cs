using BigHW1.Entities;
namespace BigHW1.ExportData
{
    public abstract class Visitor
    {
        protected readonly string _filePath;
        protected List<string> _lines = new List<string>();

        public Visitor (string filePath)
        {
            if (!File.Exists (filePath))
            {
                throw new FileNotFoundException ("Файл для записи не найден"); 
            }
            _filePath = filePath;
        }
        public abstract void Visit(BankAccount bankAccount);
        public abstract void Visit(Category category);
        public abstract void Visit(Operation operation);
        public abstract void Save();

    }
}
