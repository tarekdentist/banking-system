using System;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models.Entities
{
    // Transactions Table Schema
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; } // Unique identifier for the transaction.
        public int AccountId { get; set; } // The ID of the account associated with the transaction.
        public decimal Amount { get; set; } // The amount involved in the transaction.
        public required string TransactionType { get; set; } // Type of transaction (Deposit, Withdraw, Transfer).
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Time of the transaction.
    }
}