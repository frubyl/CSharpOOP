using BigHW1.ImportData;

namespace BigHW1.Commands.ImportCommands
{
    public class JsonImportCommand : ICommand
    {
        private readonly string _filePath;
        public JsonImporter _importer;
        public JsonImportCommand(string filePath)
        {
            _filePath = filePath;
            _importer = new JsonImporter();
        }

        public void Execute()
        {
            try
            {
                _importer.Import(_filePath);
                Console.WriteLine("Импорт из JSON выполнен.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
