# Banking System API Documentation

## Overview
This API provides a backend banking system built using ASP.NET Core and Entity Framework Core. It supports basic banking operations such as:
- Creating accounts
- Depositing and withdrawing funds
- Transferring money between accounts
- Calculating interest
- Retrieving account balances

All account and transaction data are persisted in a relational database.

## Base URL
```
http://localhost:5000/api/banking
```

## Authentication
This API does not currently implement authentication. In a production environment, authentication and authorization should be enforced.

---

## Endpoints
### 1. Create Account
**Endpoint:**
```
POST /api/banking/create
```
**Description:** Creates a new account of type CheckingAccount or SavingsAccount.

**Request Parameters:**
| Parameter    | Type   | Required | Description                                  |
|-------------|--------|----------|----------------------------------------------|
| accountType | string | Yes      | Type of account: "checking" or "savings"   |
| initialBalance | decimal | No  | Initial deposit amount (default: 0)        |

**Response:**
```json
{
    "AccountId": 1,
    "Balance": 1000,
    "Type": "savings"
}
```
---

### 2. Deposit Funds
**Endpoint:**
```
POST /api/banking/deposit
```
**Description:** Deposits funds into the specified account.

**Request Parameters:**
| Parameter  | Type    | Required | Description              |
|-----------|---------|----------|--------------------------|
| accountId | int     | Yes      | The ID of the account   |
| amount    | decimal | Yes      | Amount to deposit       |

**Response:**
```json
{
    "AccountId": 1,
    "Balance": 2000
}
```
---

### 3. Withdraw Funds
**Endpoint:**
```
POST /api/banking/withdraw
```
**Description:** Withdraws funds from an account with account-specific restrictions applied.

**Request Parameters:**
| Parameter  | Type    | Required | Description              |
|-----------|---------|----------|--------------------------|
| accountId | int     | Yes      | The ID of the account   |
| amount    | decimal | Yes      | Amount to withdraw      |

**Response:**
```json
{
    "AccountId": 1,
    "Balance": 500
}
```
---

### 4. Transfer Funds
**Endpoint:**
```
POST /api/banking/transfer
```
**Description:** Transfers funds between two accounts.

**Request Parameters:**
| Parameter    | Type    | Required | Description                     |
|-------------|---------|----------|---------------------------------|
| fromAccountId | int   | Yes      | ID of the sender's account     |
| toAccountId   | int   | Yes      | ID of the receiver's account   |
| amount        | decimal | Yes   | Amount to transfer            |

**Response:**
```json
{
    "FromAccount": {
        "AccountId": 1,
        "Balance": 300
    },
    "ToAccount": {
        "AccountId": 2,
        "Balance": 1500
    }
}
```
---

### 5. Retrieve Account Balance
**Endpoint:**
```
GET /api/banking/{id}/balance
```
**Description:** Retrieves the current balance of a specified account.

**Path Parameter:**
| Parameter | Type | Required | Description               |
|-----------|------|----------|---------------------------|
| id        | int  | Yes      | The ID of the account    |

**Response:**
```json
{
    "AccountId": 1,
    "Balance": 1500
}
```
---

### 6. Accrue Interest
**Endpoint:**
```
POST /api/banking/interest
```
**Description:** Accrues interest for a SavingsAccount over a specified number of months.

**Request Parameters:**
| Parameter  | Type    | Required | Description                       |
|-----------|---------|----------|-----------------------------------|
| accountId | int     | Yes      | The ID of the account            |
| months    | int     | Yes      | Number of months to accrue interest |

**Response:**
```json
{
    "AccountId": 1,
    "NewBalance": 1100,
    "Message": "Successfully accrued interest for 6 months"
}
```
---

## Database Schema
### 1. Accounts Table
| Column      | Type    | Description                                     |
|------------|---------|-------------------------------------------------|
| AccountId  | int     | Unique identifier for the account              |
| Balance    | decimal | Current balance in the account                 |
| AccountType | string | Specifies "checking" or "savings"               |

### 2. Transactions Table
| Column        | Type    | Description                                      |
|--------------|---------|--------------------------------------------------|
| TransactionId | int     | Unique identifier for the transaction           |
| AccountId    | int     | Foreign key referencing the related account      |
| TransactionType | string | Type of transaction (Deposit, Withdraw, Transfer, Interest Accrual) |
| Amount       | decimal | The monetary value of the transaction           |
| Timestamp    | datetime | Time of the transaction                        |

---

## Setup Instructions
1. Ensure that you have the .NET SDK installed.
2. Run the shell script provided in the project files to set up the database.
3. Use Swagger to test the API by navigating to:
```
http://localhost:5000/swagger
```

---

## Notes
- Interest is manually accrued through an API call rather than being automatically calculated over time.
- Overdraft is allowed only for CheckingAccounts, up to a predefined limit (e.g., $500).

This concludes the API documentation for the Banking System.

