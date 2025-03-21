using BigHW1.Commands;
using BigHW1.Commands.AnalyticsCommands;
using BigHW1.Commands.DataManagementCommands;
using BigHW1.Commands.ExportCommands;
using BigHW1.Commands.ImportCommands;
using BigHW1.ImportData;

namespace BigHW1.Facades
{
    public class FinanceFacade
    {
        public BankAccountFacade _bankAccountFacade;
        public CategoryFacade _categoryFacade;
        public OperationFacade _operationFacade;

        public FinanceFacade()
        {
            _bankAccountFacade = new BankAccountFacade();
            _categoryFacade = new CategoryFacade();
            _operationFacade = new OperationFacade();
        }
        // Общий метод для выполнения команд с возможностью добавления статистики
        private void ExecuteCommand(ICommand command, bool needStatistic)
        {
            if (needStatistic)
            {
                TimedCommandDecorator decorator = new TimedCommandDecorator(command);
                decorator.Execute();
            }
            else
            {
                command.Execute();
            }
        }

        // Выполнение команд для аналитики
        // Разница доходов и расходов за определенный период
        public void ExecuteCalculateNetIncomeCommand(DateTime startDate, DateTime endDate, bool needStatistic = false)
        {
            CalculateNetIncomeCommand command = new CalculateNetIncomeCommand(_operationFacade._operations, startDate, endDate);
            ExecuteCommand(command, needStatistic);
        }

        // Группировка доходов и расходов по категориям
        public void ExecuteGroupByCategoryCommand(bool needStatistic = false)
        {
            GroupByCategoryCommand command = new GroupByCategoryCommand(_operationFacade._operations, _categoryFacade._categories);
            ExecuteCommand(command, needStatistic);
        }

        // Перерасчет счета
        public void ExecuteRecalculateBalanceCommand(Guid bankAccountId, bool needStatistic = false)
        {
            RecalculateBalanceCommand command = new RecalculateBalanceCommand(_bankAccountFacade._bankAccounts, _operationFacade._operations, bankAccountId);
            try
            {
                ExecuteCommand(command, needStatistic);
                _bankAccountFacade._bankAccounts = command._bankAccounts;
            }
            catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);  
            }
        }

        // Выполнение комманд для импорта данных
        private void UpdateFieldsAfterImport(DataImporter dataImporter)
        {
            _bankAccountFacade._bankAccounts = dataImporter._bankAccounts;
            _categoryFacade._categories = dataImporter._categories;
            _operationFacade._operations = dataImporter._operations;
        }

        // Ипорт в CSV
        public void ExecuteCsvImportCommand(string filePath, bool needStatistic = false)
        {
            CsvImportCommand command = new CsvImportCommand(filePath);
            ExecuteCommand(command, needStatistic);
            UpdateFieldsAfterImport(command._importer);
        }

        // Ипорт в YAML
        public void ExecuteYamlImportCommand(string filePath, bool needStatistic = false)
        {
            YamlImportCommand command = new YamlImportCommand(filePath);
            ExecuteCommand(command, needStatistic);
            UpdateFieldsAfterImport(command._importer);
        }

        // Ипорт в JSON
        public void ExecuteJsonImportCommand(string filePath, bool needStatistic = false)
        {
            JsonImportCommand command = new JsonImportCommand(filePath);
            ExecuteCommand(command, needStatistic);
            UpdateFieldsAfterImport(command._importer);
        }

        // Выполнение комманд для экспорта данных 
        // Экспорт в CSV
        public void ExecuteCsvExportCommand(string filePath, bool needStatistic = false)
        {
            CsvExportCommand command = new CsvExportCommand(filePath, _bankAccountFacade._bankAccounts, _categoryFacade._categories, _operationFacade._operations);
            ExecuteCommand(command, needStatistic);
        }

        // Экспорт в YAML
        public void ExecuteYamlExportCommand(string filePath, bool needStatistic = false)
        {
            YamlExportCommand command = new YamlExportCommand(filePath, _bankAccountFacade._bankAccounts, _categoryFacade._categories, _operationFacade._operations);
            ExecuteCommand(command, needStatistic);
        }

        // Экспорт в JSON
        public void ExecuteJsonExportCommand(string filePath, bool needStatistic = false)
        {
            JsonExportCommand command = new JsonExportCommand(filePath, _bankAccountFacade._bankAccounts, _categoryFacade._categories, _operationFacade._operations);
            ExecuteCommand(command, needStatistic);
        }

    }
}