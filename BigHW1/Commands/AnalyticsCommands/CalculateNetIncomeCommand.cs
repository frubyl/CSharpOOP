using BigHW1.Entities;
using System.Collections.Generic;

namespace BigHW1.Commands.AnalyticsCommands
{
    public class CalculateNetIncomeCommand : ICommand
    {
        private readonly List<Operation> _operations;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public CalculateNetIncomeCommand(
            List<Operation> operations,
            DateTime startDate,
            DateTime endDate)
        {
            _operations = operations;
            _startDate = startDate;
            _endDate = endDate;
        }

        public void Execute()
        {

            var filteredOperations = _operations
                .Where(o => o.Date >= _startDate && o.Date <= _endDate)
                .ToList();

            if (!filteredOperations.Any())
            {
                Console.WriteLine("Нет операций за данный период.");
                return;
            }

            decimal income = filteredOperations
                .Where(o => o.Type == "Income")
                .Sum(o => o.Amount);

            decimal expenses = filteredOperations
                .Where(o => o.Type == "Expense")
                .Sum(o => o.Amount);

            decimal netIncome = income - expenses;

            Console.WriteLine($"Разница доходов и расходов за период с {_startDate.ToShortDateString()} по {_endDate.ToShortDateString()}: {netIncome}");
        }
    }
}