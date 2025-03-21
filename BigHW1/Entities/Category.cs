using BigHW1.ExportData;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace BigHW1.Entities
{
    public class Category : IElement
    {
        [JsonProperty]
        [YamlMember(Alias = "Id", Order = 1)]
        public Guid Id { get;  set; }
        [JsonProperty]
        [YamlMember(Alias = "Name", Order = 2)]
        public string Name { get; set; }
        [JsonProperty]
        [YamlMember(Alias = "Type", Order = 3)]

        public string Type { get;  set; } // "Income" или "Expense"

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
        public Category(string name, string type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }
        // Для импорта из CSV
        public Category() { }
    }
}
