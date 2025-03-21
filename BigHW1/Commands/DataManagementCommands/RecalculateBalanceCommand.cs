using BigHW1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigHW1.Commands.DataManagementCommands
{
    public class RecalculateBalanceCommand : ICommand
    {
        public List<BankAccount> _bankAccounts;
        private readonly List<Operation> _operations;
        private readonly Guid _accountId;

        public RecalculateBalanceCommand(
            List<BankAccount> bankAccounts,
            List<Operation> operations,
            Guid accountId)
        {
            _bankAccounts = bankAccounts;
            _operations = operations;
            _accountId = accountId;
        }

        public void Execute()
        {
            // Получаем счет по идентификатору
            var account = _bankAccounts.FirstOrDefault(a => a.Id == _accountId);
            if (account == null)
            {
                throw new ArgumentException($"Счет с ID {_accountId} не найден.");
            }

            // Фильтруем операции по счету
            var accountOperations = _operations
                .Where(o => o.BankAccountId == _accountId)
                .ToList();

            // Считаем новый баланс
            decimal newBalance = accountOperations
                .Sum(o => o.Type == "Income" ? o.Amount : -o.Amount);

            // Обновляем баланс счета
            account.Balance = newBalance;

            Console.WriteLine($"Баланс счета {account.Name} успешно пересчитан. Новый баланс: {newBalance}");
        }
    }
}