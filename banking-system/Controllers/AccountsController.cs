using System;
using BankingSystem.Data;
using BankingSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Injects the database context into the controller.
        public BankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to create a new account.
        [HttpPost("create")]
        public IActionResult CreateAccount(string accountType, decimal initialBalance = 0)
        {
            Account account;
            if (accountType.ToLower() == "checking")
            {
                account = new CheckingAccount { Balance = initialBalance };
            }
            else if (accountType.ToLower() == "savings")
            {
                account = new SavingsAccount { Balance = initialBalance };
            }
            else
            {
                return BadRequest("Invalid account type. Use 'checking' or 'savings'");
            }

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Ok(new { AccountId = account.AccountId, Balance = account.Balance, Type = accountType });
        }

        // Endpoint to deposit money into an account.
        [HttpPost("deposit")]
        public IActionResult Deposit(int accountId, decimal amount)
        {
            var account = _context.Accounts.Find(accountId);
            if (account == null) return NotFound("Account not found.");

            account.Deposit(amount);
            _context.Transactions.Add(new Transaction { AccountId = accountId, Amount = amount, TransactionType = "Deposit" });
            _context.SaveChanges();

            return Ok(account);
        }

        // Endpoint to withdraw money from an account.
        [HttpPost("withdraw")]
        public IActionResult Withdraw(int accountId, decimal amount)
        {
            var account = _context.Accounts.Find(accountId);
            if (account == null) return NotFound("Account not found.");

            try
            {
                account.Withdraw(amount);
                _context.Transactions.Add(new Transaction { AccountId = accountId, Amount = amount, TransactionType = "Withdraw" });
                _context.SaveChanges();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to transfer money between two accounts.
        [HttpPost("transfer")]
        public IActionResult Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _context.Accounts.Find(fromAccountId);
            var toAccount = _context.Accounts.Find(toAccountId);

            if (fromAccount == null || toAccount == null)
                return NotFound("One or both accounts not found.");

            try
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);

                _context.Transactions.Add(new Transaction { AccountId = fromAccountId, Amount = -amount, TransactionType = "Transfer Out" });
                _context.Transactions.Add(new Transaction { AccountId = toAccountId, Amount = amount, TransactionType = "Transfer In" });

                _context.SaveChanges();
                return Ok(new { FromAccount = fromAccount, ToAccount = toAccount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to retrieve the balance of an account.
        [HttpGet("{id}/balance")]
        public IActionResult GetBalance(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null) return NotFound("Account not found.");

            return Ok(new { AccountId = id, Balance = account.Balance });
        }

        // Endpooint to calculate interent and deposit it to an account.
        [HttpPost("interest")]
        public IActionResult Interest(int accountId, int months)
        {
            var account = _context.Accounts.Find(accountId);

            // Error checking and making sure it is a checking account.
            if (account == null) 
                return NotFound("Account not found.");

            if (account is not SavingsAccount savingsAccount)
                return BadRequest("Interest can only be accrued on savings accounts.");

            try
            {
                savingsAccount.Interest(months);
                
                _context.Transactions.Add(new Transaction 
                { 
                    AccountId = accountId,
                    Amount = savingsAccount.Balance - account.Balance,
                    TransactionType = "Interest Accrual",
                    Timestamp = DateTime.UtcNow
                });
                
                _context.SaveChanges();

                return Ok(new { 
                    AccountId = accountId, 
                    NewBalance = savingsAccount.Balance,
                    Message = $"Successfully accrued interest for {months} months"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}