# 💰 Financial Accounting Module

**A console app for managing personal finances, implemented in C# using OOP, GRASP principles, and GoF design patterns.**

---

## ✨ Core Functionality

- Create, update, and delete:
  - 💼 **Bank Accounts**
  - 🗂 **Categories** (income/expense)
  - 📄 **Operations** (income/expense transactions)

---

## ⚙️ Optional Features

### 📊 Analytics
- Calculate income/expense difference over a selected period
- Group transactions by category

### 🔁 Import/Export
- Export data to CSV, YAML, or JSON
- Import data from these formats

### 🧮 Data Management
- Recalculate account balances to fix inconsistencies

### 📈 Statistics
- Measure execution time of user scenarios

---

## 🧱 Data Model

### `BankAccount`
- `id`: Unique account ID
- `name`: Account name
- `balance`: Current account balance

### `Category`
- `id`: Unique category ID
- `type`: Income or expense
- `name`: Category name (e.g., "Salary", "Café")

### `Operation`
- `id`: Unique operation ID
- `type`: Income or expense
- `bank_account_id`: Reference to the related bank account
- `amount`: Operation amount
- `date`: Operation date
- `description`: Optional description
- `category_id`: Reference to the related category

---

## 🧩 Design Patterns Used

- **Facade**: Separate logic handlers for `BankAccount`, `Category`, `Operation`, and Analytics
- **Command + Decorator**: All user scenarios are implemented as commands, wrapped in a decorator to measure execution time
- **Template Method**: Used for unified data import structure across CSV, YAML, and JSON
- **Visitor**: Export logic handled via visitor implementations
- **Factory**: Centralized creation and validation of domain objects

---

## 📦 Technologies & Principles

- ✅ **C# / .NET Console App**
- ✅ **Object-Oriented Programming (OOP)**
- ✅ **GRASP Concepts (High Cohesion, Low Coupling)**
- ✅ **GoF Design Patterns**
- ✅ **Unit Testing**, **Dependency Injection Container**

---

## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/frubyl/CSharpOOP.git
   ```

2. Open the solution in your IDE (Visual Studio or Rider).

3. Build and run the project.

4. Follow console prompts to interact with the financial accounting module.

---

## 📄 Report Summary

- This project demonstrates an object-oriented approach to implementing a personal finance tracking tool.
- It covers both required and optional functionality.
- GRASP principles help structure the code with high cohesion and low coupling.
- GoF design patterns are used to organize logic and add flexibility.

---

**Made with 💸, 💻 and 💡 by [frubyl](https://github.com/frubyl)**

