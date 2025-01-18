### Banking System

This project is a banking system backend built using ASP.NET Core and Entity Framework Core. It simulates basic banking operations such as depositing, withdrawing, transferring money, calculating interest, and retrieving account balances, while persisting account and transaction data to a relational database. 

## Entities Design

I designed this project with the following 4 main entities, which are the base for everything I built upon them:

1. **Account**  
   This entity is an abstract class that includes the essential attributes any account should have, such as `AccountId` and `Balance`. It provides flexibility to introduce more account types in the future based on this abstract class.  
   - It defines an abstract function `Withdraw`, which derived classes implement in their own way.  
   - It also includes a `Deposit` function to add funds to the account.

2. **CheckingAccount**  
   This entity inherits from the `Account` class and implements specific logic for checking accounts:  
   - Allows overdrafts up to a predefined limit (e.g., $500).  
   - Overrides the `Withdraw` function to enforce overdraft rules.

3. **SavingsAccount**  
   This entity also inherits from the `Account` class and includes specific functionality for savings accounts:  
   - Does not allow withdrawals exceeding the available balance.  
   - Supports interest accrual over time based on the account balance.  
   - Overrides the `Withdraw` function to enforce withdrawal restrictions.

4. **Transaction**  
   This entity represents a record of all account activities, such as deposits, withdrawals, and transfers. It includes attributes like:  
   - `TransactionId`, `TransactionType`, `Amount`, and `Timestamp`.  
   - Transactions are linked to the relevant accounts for tracking purposes.

## Controllers

The `AccountsController` serves as the core API controller, providing endpoints for interacting with the system. It uses dependency injection to integrate with the `ApplicationDbContext` for database operations.

Here are the API endpoints it exposes:

1. Create Account
- Creates a new account of type `CheckingAccount` or `SavingsAccount`.
- Adds the account to the database and returns the account details.

2. Deposit
- Deposits funds into the specified account.
- Records the transaction in the database.

3. Withdraw
- Withdraws funds from the specified account.
- Enforces account-specific rules (e.g., overdraft limits for `CheckingAccount` or balance restrictions for `SavingsAccount`).
- Records the transaction in the database.

4. Transfer
- Transfers funds between two accounts.
- Validates account existence and sufficient balance.
- Records the transfer as two separate transactions: "Transfer Out" and "Transfer In."

5. Retrieve Balance
- Retrieves the current balance of the specified account ID. 

6. Interest
- Accrues interest for a `SavingsAccount` over a specified number of months.
- Records the interest accrual as a transaction in the database.
`Normaally I would make the interest be calculated automatically based on the balance and the time passed but I did it this way for simplicity and testing purposes`

## Database Design

The database schema includes the following tables:

1. Accounts
- Stores account details, including:
  - `AccountId`: Unique identifier.
  - `Balance`: Current balance.
  - `AccountType`: Indicates whether the account is a `CheckingAccount` or `SavingsAccount`.

2. Transactions
- Logs all account activities with attributes such as:
  - `TransactionId`: Unique identifier.
  - `TransactionType`: Type of transaction (e.g., "Deposit", "Withdraw").
  - `Amount`: The monetary value involved.
  - `Timestamp`: Time of the transaction.


`To set up the database, please make sure that you have .NET SDK installed and run the shell script provided in the project's files.`

`To test the project's API's, make sure you have Swagger installed and visit <localhost-domain>/swagger`
