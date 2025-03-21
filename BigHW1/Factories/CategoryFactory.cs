using BigHW1.Entities;

namespace BigHW1.Factories
{
    public class CategoryFactory
    {
        public Category CreateCategory(string name, string type)
        {
            if (type != "Income" && type != "Expense")
            {
                throw new ArgumentException("Тип категории должен быть либо 'Income', либо 'Expense'.");
            }

            return new Category(name, type); 
        }
    }

}
