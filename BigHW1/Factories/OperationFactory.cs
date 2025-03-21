using BigHW1.Entities;

namespace BigHW1.Factories
{
    public class OperationFactory
    {
        public Operation CreateOperation(Guid bankAccountId, Guid categoryId, decimal amount, string type, DateTime date, string description = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма операции должна быть положительной.");
            }

            if (type != "Income" && type != "Expense")
            {
                throw new ArgumentException("Тип операции должен быть либо 'Income', либо 'Expense'.");
            }

            return new Operation(type, bankAccountId, amount, date, categoryId, description); 
        }
    }

}
