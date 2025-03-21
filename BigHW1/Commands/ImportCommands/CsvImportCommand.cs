using BigHW1.ImportData;

namespace BigHW1.Commands.ImportCommands
{
    public class CsvImportCommand : ICommand
    {
        private readonly string _filePath;
        public CsvImporter _importer;
        public CsvImportCommand(string filePath)
        {
            _filePath = filePath;
            _importer = new CsvImporter();
        }

        public void Execute()
        {
            try
            {
                _importer.Import(_filePath);
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
