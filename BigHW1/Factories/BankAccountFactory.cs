using BigHW1.Entities;

namespace BigHW1.Factories
{
    public class BankAccountFactory
    {
        public BankAccount CreateAccount(string name, decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Начальный баланс не может быть отрицательным.");
            }

            return new BankAccount(name, initialBalance); 
        }
    }
}
