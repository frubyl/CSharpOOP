using BigHW1.ImportData;

namespace BigHW1.Commands.ImportCommands
{
    public class YamlImportCommand : ICommand
    {
        private readonly string _filePath;
        public YamlImporter _importer;
        public YamlImportCommand(string filePath)
        {
            _filePath = filePath;
            _importer = new YamlImporter();
        }

        public void Execute()
        {
            try
            {
                _importer.Import(_filePath);
                Console.WriteLine("Импорт из YAML выполнен.");
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
