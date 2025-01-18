using System;

namespace BankingSystem.Models.Entities
{
    // Checking account class
    public class CheckingAccount : Account
    {
        // Allowing this account type to have an overdraft of 500.
        public decimal OverdraftLimit { get; set; } = 500m; 

        // Overrides the Withdraw method from the main Account class to allow overdrafts within the limit.
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be greater than zero.");
            if (Balance - amount < -OverdraftLimit)
                throw new InvalidOperationException("Overdraft limit exceeded.");
            Balance -= amount;
        }
    }
}