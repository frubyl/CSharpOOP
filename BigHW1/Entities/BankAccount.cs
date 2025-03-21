
using BigHW1.ExportData;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace BigHW1.Entities
{
    public class BankAccount : IElement {
        [JsonProperty]
        [YamlMember(Alias = "Id", Order = 1)]
        public Guid Id { get;  set; }
        [JsonProperty]
        [YamlMember(Alias = "Name", Order = 2)]
        public string Name { get; set; }
        [JsonProperty]
        [YamlMember(Alias = "Balance", Order = 3)]
        public decimal Balance { get; set; }
        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
        public BankAccount(string name, decimal initialBalance)
        {
            Id = Guid.NewGuid();
            Name = name;
            Balance = initialBalance;
        }
        // Для импорта из CSV
        public BankAccount() { }    
    }
}
