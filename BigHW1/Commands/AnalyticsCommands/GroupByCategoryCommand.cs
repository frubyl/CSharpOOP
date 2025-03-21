using BigHW1.Entities;

namespace BigHW1.Commands.AnalyticsCommands
{
    public class GroupByCategoryCommand : ICommand
    {
        private readonly List<Operation> _operations;
        private readonly List<Category> _categories;

        public GroupByCategoryCommand(
            List<Operation> operations,
            List<Category> categories)
        {
            _operations = operations;
            _categories = categories;
        }

        public void Execute()
        {
            // Группируем операции по категориям
            var groupedOperations = _operations
                .GroupBy(o => o.CategoryId)
                .Select(g => new
                {
                    CategoryName = _categories.First(c => c.Id == g.Key).Name,
                    Income = g.Where(o => o.Type == "Income").Sum(o => o.Amount),
                    Expenses = g.Where(o => o.Type == "Expense").Sum(o => o.Amount)
                })
                .ToList();

            if (!groupedOperations.Any())
            {
                Console.WriteLine("Не удалось разбить операции по категориям.");
                return;
            }
            // Выводим результат
            Console.WriteLine("Группировка доходов и расходов по категориям:");
            foreach (var group in groupedOperations)
            {
                Console.WriteLine($"Категория: {group.CategoryName}");
                Console.WriteLine($"  Доходы: {group.Income}");
                Console.WriteLine($"  Расходы: {group.Expenses}");
                Console.WriteLine();
            }
        }
    }
}