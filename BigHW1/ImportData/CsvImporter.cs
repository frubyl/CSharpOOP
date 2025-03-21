// Файл: CsvImporter.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using BigHW1.Entities;

namespace BigHW1.ImportData
{
    // Реализация CSV-импортера
    public class CsvImporter : DataImporter
    {  
        protected override void ParseData(string data)
        {
            // Разбиваем весь текст на секции по пустым строкам
            var sections = data.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var section in sections)
            {
                // Разбиваем секцию на строки
                var lines = section.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2)
                    continue;
                // Первая строка – название секции: BankAccounts, Categories или Operations
                string sectionName = lines[0].Trim();
                // Вторая строка – строка заголовка CSV для этой секции
                string header = lines[1].Trim();

                var sb = new StringBuilder();
                sb.AppendLine(header);
                for (int i = 2; i < lines.Length; i++)
                {
                    sb.AppendLine(lines[i]);
                }
                string sectionData = sb.ToString();

                using (var reader = new StringReader(sectionData))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                }))
                {
                    if (sectionName == "BankAccounts")
                    {
                        csv.Context.RegisterClassMap<BankAccountMap>();
                        var accounts = csv.GetRecords<BankAccount>().ToList();
                        _bankAccounts.AddRange(accounts);
                    }
                    else if (sectionName == "Categories")
                    {
                        csv.Context.RegisterClassMap<CategoryMap>();
                        var categories = csv.GetRecords<Category>().ToList();
                        _categories.AddRange(categories);
                    }
                    else if (sectionName == "Operations")
                    {
                        csv.Context.RegisterClassMap<OperationMap>();
                        var operations = csv.GetRecords<Operation>().ToList();
                        _operations.AddRange(operations);
                    }
                }
            }
        }
    }

    // Маппинг для BankAccount – ожидается строка с заголовками: Id,Name,Balance
    public class BankAccountMap : ClassMap<BankAccount>
    {
        public BankAccountMap()
        {
            Map(m => m.Id)
                .Name("Id")
                .Convert(args => Guid.Parse(args.Row.GetField("Id")));
            Map(m => m.Name)
                .Name("Name");
            Map(m => m.Balance)
                .Name("Balance")
                .Convert(args => decimal.Parse(args.Row.GetField("Balance"), CultureInfo.InvariantCulture));
        }
    }

    // Маппинг для Category – ожидается строка с заголовками: Id,Name,Type
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Map(m => m.Id)
                .Name("Id")
                .Convert(args => Guid.Parse(args.Row.GetField("Id")));
            Map(m => m.Name)
                .Name("Name");
            Map(m => m.Type)
                .Name("Type")
                .Convert(args => args.Row.GetField("Type"));
        }
    }

    // Маппинг для Operation – ожидается строка с заголовками: Id,Type,BankAccountId,Amount,Date,CategoryId,Description
    public class OperationMap : ClassMap<Operation>
    {
        public OperationMap()
        {
            Map(m => m.Id)
                .Name("Id")
                .Convert(args => Guid.Parse(args.Row.GetField("Id")));
            Map(m => m.Type)
                .Name("Type")
                .Convert(args => args.Row.GetField("Type"));
            Map(m => m.BankAccountId)
                .Name("BankAccountId")
                .Convert(args => Guid.Parse(args.Row.GetField("BankAccountId")));
            Map(m => m.Amount)
                .Name("Amount")
                .Convert(args => decimal.Parse(args.Row.GetField("Amount"), CultureInfo.InvariantCulture));
            Map(m => m.Date)
                .Name("Date")
                .Convert(args => DateTime.ParseExact(args.Row.GetField("Date"), "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            Map(m => m.CategoryId)
                .Name("CategoryId")
                .Convert(args => Guid.Parse(args.Row.GetField("CategoryId")));
            Map(m => m.Description)
                .Name("Description")
                .Optional();
        }
    }
}
