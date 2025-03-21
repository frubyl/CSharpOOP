using BigHW1.Entities;
using BigHW1.Factories;

namespace BigHW1.Facades
{
    public class BankAccountFacade
    {
        public List<BankAccount> _bankAccounts { get; set; }

        public BankAccountFacade()
        {
            _bankAccounts = new List<BankAccount>();
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            _bankAccounts.Add(bankAccount);
        }
        public void EditBankAccount(Guid bankAccountId, string newName)
        {
            var bankAccount = _bankAccounts.FirstOrDefault(c => c.Id == bankAccountId);
            if (bankAccount == null)
            {
                throw new InvalidOperationException("Банковский аккаунт с таким ID не найдена");
            }
            bankAccount.Name = newName;
            
        }
        public void DeleteBankAccount(Guid accountId)
        {
            var bankAccount = _bankAccounts.FirstOrDefault(a => a.Id == accountId);
            if (bankAccount == null)
            {
                throw new InvalidOperationException("Банковский аккаунт с таким ID не найдена");
            }
            _bankAccounts.Remove(bankAccount);
        }
    }

}
