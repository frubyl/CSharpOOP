using BigHW1.ExportData;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace BigHW1.Entities
{
    public class Operation : IElement
    {
        [JsonProperty]
        [YamlMember(Alias = "Id", Order = 1)]
        public Guid Id { get;   set; }
        [JsonProperty]
        [YamlMember(Alias = "Type", Order = 2)]
        public string Type { get;  set; } // "Income" или "Expense"
        [JsonProperty]
        [YamlMember(Alias = "BankAccountId", Order = 3)]
        public Guid BankAccountId { get;  set; }
        [JsonProperty]
        [YamlMember(Alias = "Amount", Order = 4)]
        public decimal Amount { get;  set; }
        [JsonProperty]
        [YamlMember(Alias = "Date", Order = 5)]
        public DateTime Date { get;  set; }
        [JsonProperty]
        [YamlMember(Alias = "Description", Order = 6)]
        public string Description { get; set; }
        [JsonProperty]
        [YamlMember(Alias = "CategoryId", Order = 7)]
        public Guid CategoryId { get;  set; }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
        public Operation(string type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string description = null)
        {
            Id = Guid.NewGuid();
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }

        // Для импорта из CSV
        public Operation() { }
    }
}
