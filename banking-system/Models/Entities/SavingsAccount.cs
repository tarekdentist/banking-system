using System;

namespace BankingSystem.Models.Entities
{
    // Savings account class
    public class SavingsAccount : Account
    {
    public decimal InterestRate { get; set; } = 0.05m; // 5% annual interest rate

    // Function for calculating interest based on months passed and account balance.
    public void Interest(int months)
    {
        if (months <= 0) throw new ArgumentException("Months must be greater than zero.");
        
        // Calculating monthly interest rate (annual rate / 12).
        decimal monthlyRate = InterestRate / 12;
        
        // Calculating interest for the specified months.
        decimal interest = Balance * monthlyRate * months;
        
        // Adding interest to balance.
        Deposit(interest);
    }
    // Overrides the Withdraw method from the main Account class with no overdrafts. 
    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be greater than zero.");
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }
    }
}