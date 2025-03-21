using BigHW1.Entities;
using System;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace BigHW1.ImportData
{
    public class YamlImporter : DataImporter
    {
        protected override void ParseData(string data)
        {
            var input = new StringReader(data);
            var yaml = new YamlStream();
            yaml.Load(input);

            var rootNode = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Обработка BankAccounts
            if (rootNode.Children.ContainsKey("BankAccounts"))
            {
                var accountsNode = (YamlSequenceNode)rootNode.Children["BankAccounts"];
                foreach (var node in accountsNode)
                {
                    var accountNode = (YamlMappingNode)node;
                    var id = Guid.Parse(accountNode.Children[new YamlScalarNode("Id")].ToString());  
                    var name = accountNode.Children[new YamlScalarNode("Name")].ToString();
                    var balance = decimal.Parse(accountNode.Children[new YamlScalarNode("Balance")].ToString());

                    var account = new BankAccount(name, balance) { Id = id };  
                    _bankAccounts.Add(account);
                }
            }

            // Обработка Categories
            if (rootNode.Children.ContainsKey("Categories"))
            {
                var categoriesNode = (YamlSequenceNode)rootNode.Children["Categories"];
                foreach (var node in categoriesNode)
                {
                    var categoryNode = (YamlMappingNode)node;
                    var id = Guid.Parse(categoryNode.Children[new YamlScalarNode("Id")].ToString()); 
                    var name = categoryNode.Children[new YamlScalarNode("Name")].ToString();
                    var type = categoryNode.Children[new YamlScalarNode("Type")].ToString();

                    var category = new Category(name, type) { Id = id };  
                    _categories.Add(category);
                }
            }

            // Обработка Operations
            if (rootNode.Children.ContainsKey("Operations"))
            {
                var operationsNode = (YamlSequenceNode)rootNode.Children["Operations"];
                foreach (var node in operationsNode)
                {
                    var operationNode = (YamlMappingNode)node;
                    var id = Guid.Parse(operationNode.Children[new YamlScalarNode("Id")].ToString());  
                    var type = operationNode.Children[new YamlScalarNode("Type")].ToString();
                    var bankAccountId = Guid.Parse(operationNode.Children[new YamlScalarNode("BankAccountId")].ToString());
                    var amount = decimal.Parse(operationNode.Children[new YamlScalarNode("Amount")].ToString());
                    var date = DateTime.Parse(operationNode.Children[new YamlScalarNode("Date")].ToString());
                    var categoryId = Guid.Parse(operationNode.Children[new YamlScalarNode("CategoryId")].ToString());
                    var description = operationNode.Children[new YamlScalarNode("Description")]?.ToString();

                    var operation = new Operation(type, bankAccountId, amount, date, categoryId, description) { Id = id };  
                    _operations.Add(operation);
                }
            }
        }
    }
}
