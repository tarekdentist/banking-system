using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Transactions;
using BankingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Configures inheritance mapping for account types.
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // Adding the account type to the Accounts table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<CheckingAccount>("Checking")
                .HasValue<SavingsAccount>("Savings");
        }
    }
}