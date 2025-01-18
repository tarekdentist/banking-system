using System;
using System.ComponentModel.DataAnnotations;


namespace BankingSystem.Models.Entities
{
    // Main account class which other account types will inherit from.
    public abstract class Account
    {
        [Key]
        public int AccountId { get; set; } // Unique identifier for the account.
        public decimal Balance { get; set; } // Balance for the account

        // Abstract method to enforce withdrawal logic in other account types. 
        public abstract void Withdraw(decimal amount);

        // Method to deposit money into the account.
        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");
            Balance += amount;
        }
    }
}