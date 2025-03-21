
using Microsoft.Extensions.DependencyInjection;
using BigHW1.Entities;
using BigHW1.Facades;
using BigHW1.Factories;

namespace BigHW1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Настраиваем DI-контейнер и получаем необходимые зависимости
            var serviceProvider = ConfigureAndBuildServiceProvider();
            var financeFacade = serviceProvider.GetService<FinanceFacade>();
            var bankAccountFactory = serviceProvider.GetService<BankAccountFactory>();
            var categoryFactory = serviceProvider.GetService<CategoryFactory>();
            var operationFactory = serviceProvider.GetService<OperationFactory>();

            // 2. Создаём тестовые данные через фабрики и добавляем их через фасады
            Console.WriteLine("========== ШАГ 1: СОЗДАНИЕ ТЕСТОВЫХ ДАННЫХ ==========\n");
            var (account1, account2, categoryIncome, categoryExpense, operation1, operation2) =
                CreateTestData(financeFacade, bankAccountFactory, categoryFactory, operationFactory);

            // Выводим созданные данные в консоль
            DisplayTestData(financeFacade);

            // 3. Аналитика (расчёт чистого дохода и группировка по категориям)
            Console.WriteLine("\n========== ШАГ 2: АНАЛИТИКА (без статистики) ==========\n");
            financeFacade.ExecuteCalculateNetIncomeCommand(DateTime.Now.AddDays(-10), DateTime.Now, false);
            financeFacade.ExecuteGroupByCategoryCommand(false);

            Console.WriteLine("\n========== ШАГ 3: АНАЛИТИКА (со статистикой) ==========\n");
            financeFacade.ExecuteCalculateNetIncomeCommand(DateTime.Now.AddDays(-10), DateTime.Now, true);
            financeFacade.ExecuteGroupByCategoryCommand(true);

            // 4. Перерасчёт баланса
            Console.WriteLine("\n========== ШАГ 4: ПЕРЕРАСЧЁТ БАЛАНСА (без статистики) ==========\n");
            financeFacade.ExecuteRecalculateBalanceCommand(account1.Id, false);

            Console.WriteLine("\n========== ШАГ 5: ПЕРЕРАСЧЁТ БАЛАНСА (со статистикой) ==========\n");
            financeFacade.ExecuteRecalculateBalanceCommand(account1.Id, true);

            // 5. Подготавливаем пути для экспорта в TestData (в корне проекта)
            string testDataFolder = GetTestDataFolder();
            string csvFilePath = Path.Combine(testDataFolder, "export.csv");
            string jsonFilePath = Path.Combine(testDataFolder, "export.json");
            string yamlFilePath = Path.Combine(testDataFolder, "export.yaml");

            // 6. Экспорт данных
            Console.WriteLine("\n========== ШАГ 6: ЭКСПОРТ ДАННЫХ (без статистики) ==========\n");
            financeFacade.ExecuteCsvExportCommand(csvFilePath, false);
            financeFacade.ExecuteYamlExportCommand(yamlFilePath, false);
            financeFacade.ExecuteJsonExportCommand(jsonFilePath, false);

            Console.WriteLine("\n========== ШАГ 7: ЭКСПОРТ ДАННЫХ (со статистикой) ==========\n");
            financeFacade.ExecuteCsvExportCommand(csvFilePath, true);
            financeFacade.ExecuteYamlExportCommand(yamlFilePath, true);
            financeFacade.ExecuteJsonExportCommand(jsonFilePath, true);

            // 7. Импорт данных (после экспорта) для проверки обновления коллекций

            // CSV Импорт без статистики
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 8: ИМПОРТ ДАННЫХ CSV (без статистики) ==========\n");
            financeFacade.ExecuteCsvImportCommand(csvFilePath, false);
            DisplayTestData(financeFacade);

            // CSV Импорт со статистикой
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 9: ИМПОРТ ДАННЫХ CSV (со статистикой) ==========\n");
            financeFacade.ExecuteCsvImportCommand(csvFilePath, true);
            DisplayTestData(financeFacade);

            // YAML Импорт без статистики
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 10: ИМПОРТ ДАННЫХ YAML (без статистики) ==========\n");
            financeFacade.ExecuteYamlImportCommand(yamlFilePath, false);
            DisplayTestData(financeFacade);

            // YAML Импорт со статистикой
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 11: ИМПОРТ ДАННЫХ YAML (со статистикой) ==========\n");
            financeFacade.ExecuteYamlImportCommand(yamlFilePath, true);
            DisplayTestData(financeFacade);

            // JSON Импорт без статистики
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 12: ИМПОРТ ДАННЫХ JSON (без статистики) ==========\n");
            financeFacade.ExecuteJsonImportCommand(jsonFilePath, false);
            DisplayTestData(financeFacade);

            // JSON Импорт со статистикой
            financeFacade._bankAccountFacade._bankAccounts.Clear();
            financeFacade._categoryFacade._categories.Clear();
            financeFacade._operationFacade._operations.Clear();
            Console.WriteLine("\n========== ШАГ 13: ИМПОРТ ДАННЫХ JSON (со статистикой) ==========\n");
            financeFacade.ExecuteJsonImportCommand(jsonFilePath, true);
            DisplayTestData(financeFacade);

            Console.WriteLine("\n========== ВСЕ ТЕСТЫ ЗАВЕРШЕНЫ ==========");
            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        /// <summary>
        /// Конфигурирует DI-контейнер и возвращает ServiceProvider.
        /// </summary>
        private static ServiceProvider ConfigureAndBuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Регистрируем фасады как синглтоны
            services.AddSingleton<BankAccountFacade>();
            services.AddSingleton<CategoryFacade>();
            services.AddSingleton<OperationFacade>();
            services.AddSingleton<FinanceFacade>();

            // Регистрируем фабрики как синглтоны
            services.AddSingleton<BankAccountFactory>();
            services.AddSingleton<CategoryFactory>();
            services.AddSingleton<OperationFactory>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Создаёт тестовые данные через фабрики и добавляет их через фасады.
        /// Возвращает созданные объекты для дальнейшего отображения.
        /// </summary>
        private static (
            BankAccount account1,
            BankAccount account2,
            Category categoryIncome,
            Category categoryExpense,
            Operation operation1,
            Operation operation2
        ) CreateTestData(
            FinanceFacade financeFacade,
            BankAccountFactory bankAccountFactory,
            CategoryFactory categoryFactory,
            OperationFactory operationFactory)
        {
            // Создаем банковские счета
            var account1 = bankAccountFactory.CreateAccount("Account1", 1000);
            var account2 = bankAccountFactory.CreateAccount("Account2", 500);
            financeFacade._bankAccountFacade.AddBankAccount(account1);
            financeFacade._bankAccountFacade.AddBankAccount(account2);

            // Создаем категории
            var categoryIncome = categoryFactory.CreateCategory("Salary", "Income");
            var categoryExpense = categoryFactory.CreateCategory("Groceries", "Expense");
            financeFacade._categoryFacade.AddCategory(categoryIncome);
            financeFacade._categoryFacade.AddCategory(categoryExpense);

            // Создаем операции
            var operation1 = operationFactory.CreateOperation(
                account1.Id,
                categoryIncome.Id,
                2000,
                "Income",
                DateTime.Now.AddDays(-5),
                "Salary for last month"
            );
            var operation2 = operationFactory.CreateOperation(
                account1.Id,
                categoryExpense.Id,
                500,
                "Expense",
                DateTime.Now.AddDays(-3),
                "Grocery shopping"
            );
            financeFacade._operationFacade.AddOperation(operation1);
            financeFacade._operationFacade.AddOperation(operation2);

            return (account1, account2, categoryIncome, categoryExpense, operation1, operation2);
        }

        /// <summary>
        /// Выводит в консоль текущие данные (банковские счета, категории и операции) из фасадов.
        /// </summary>
        private static void DisplayTestData(FinanceFacade financeFacade)
        {
            Console.WriteLine("----- Банковские счета -----");
            foreach (var account in financeFacade._bankAccountFacade._bankAccounts)
            {
                Console.WriteLine($"Имя: {account.Name}, Баланс: {account.Balance}, ID: {account.Id}");
            }
            Console.WriteLine("\n----- Категории -----");
            foreach (var category in financeFacade._categoryFacade._categories)
            {
                Console.WriteLine($"Имя: {category.Name}, Тип: {category.Type}, ID: {category.Id}");
            }
            Console.WriteLine("\n----- Операции -----");
            foreach (var operation in financeFacade._operationFacade._operations)
            {
                Console.WriteLine($"Описание: {operation.Description}, Тип: {operation.Type}, Сумма: {operation.Amount}, Дата: {operation.Date}, ID: {operation.Id}, Description: {operation.Description}");
            }
        }

        /// <summary>
        /// Определяет путь к папке TestData в корне проекта.
        /// Поднимается на 2 уровня вверх от текущей директории сборки.
        /// </summary>
        private static string GetTestDataFolder()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Directory.GetParent(currentDir)?.Parent?.Parent?.FullName
                             ?? currentDir;
            string testDataFolder = Path.Combine(projectDir, "TestData");
            if (!Directory.Exists(testDataFolder))
            {
                Directory.CreateDirectory(testDataFolder);
            }
            return testDataFolder;
        }
    }
}
