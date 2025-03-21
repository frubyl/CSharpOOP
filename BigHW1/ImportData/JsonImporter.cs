using Newtonsoft.Json;
using BigHW1.Entities;

namespace BigHW1.ImportData
{
    public class JsonImporter : DataImporter
    {
        protected override void ParseData(string data)
        {
            var parsedData = new List<object>();

            // Десериализация JSON в словарь
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

            // Обработка BankAccounts
            if (jsonData.ContainsKey("BankAccounts"))
            {
                var accounts = JsonConvert.DeserializeObject<List<BankAccount>>(jsonData["BankAccounts"].ToString());
                foreach (var account in accounts)
                {
                    _bankAccounts.Add(account);
                }
            }

            // Обработка Categories
            if (jsonData.ContainsKey("Categories"))
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonData["Categories"].ToString());
                foreach (var category in categories)
                {
                    _categories.Add(category);
                }
            }

            // Обработка Operations
            if (jsonData.ContainsKey("Operations"))
            {
                var operations = JsonConvert.DeserializeObject<List<Operation>>(jsonData["Operations"].ToString());
                foreach (var operation in operations)
                {
                    _operations.Add(operation);
                }
            }
        }
    }
}