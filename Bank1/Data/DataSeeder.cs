using Bank1.Models;

namespace Bank1.Data
{
    public static class DataSeeder
    {
        public static void SeedData(BankDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.Accounts.Any())
            {
                return; // Data already seeded
            }

            // Sample Account data
            var accounts = new List<Account>
            {
                new Account { AccountName = "ACC001", Pincode = 1234 },
                new Account { AccountName = "ACC002", Pincode = 5678 },
                new Account { AccountName = "ACC003", Pincode = 9012 },
                new Account { AccountName = "ACC004", Pincode = 3456 },
                new Account { AccountName = "ACC005", Pincode = 7890 }
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();

            // Sample Transaction data
            var transactions = new List<Transaction>
            {
                new Transaction { TransDate = "2024-01-15 09:30:00", Withdraw = 0, Deposit = 1000.00m, Balance = 1000.00m, AccountName = "ACC001" },
                new Transaction { TransDate = "2024-01-16 14:20:00", Withdraw = 200.00m, Deposit = 0, Balance = 800.00m, AccountName = "ACC001" },
                new Transaction { TransDate = "2024-01-17 11:45:00", Withdraw = 0, Deposit = 500.00m, Balance = 1300.00m, AccountName = "ACC001" },
                new Transaction { TransDate = "2024-01-18 16:30:00", Withdraw = 150.00m, Deposit = 0, Balance = 1150.00m, AccountName = "ACC001" },
                new Transaction { TransDate = "2024-01-19 10:15:00", Withdraw = 0, Deposit = 2000.00m, Balance = 3150.00m, AccountName = "ACC001" },
                new Transaction { TransDate = "2024-01-15 08:00:00", Withdraw = 0, Deposit = 2500.00m, Balance = 2500.00m, AccountName = "ACC002" },
                new Transaction { TransDate = "2024-01-16 13:30:00", Withdraw = 300.00m, Deposit = 0, Balance = 2200.00m, AccountName = "ACC002" },
                new Transaction { TransDate = "2024-01-17 15:45:00", Withdraw = 0, Deposit = 750.00m, Balance = 2950.00m, AccountName = "ACC002" },
                new Transaction { TransDate = "2024-01-18 12:20:00", Withdraw = 100.00m, Deposit = 0, Balance = 2850.00m, AccountName = "ACC002" },
                new Transaction { TransDate = "2024-01-19 09:30:00", Withdraw = 0, Deposit = 1200.00m, Balance = 4050.00m, AccountName = "ACC002" },
                new Transaction { TransDate = "2024-01-15 10:00:00", Withdraw = 0, Deposit = 800.00m, Balance = 800.00m, AccountName = "ACC003" },
                new Transaction { TransDate = "2024-01-16 14:00:00", Withdraw = 50.00m, Deposit = 0, Balance = 750.00m, AccountName = "ACC003" },
                new Transaction { TransDate = "2024-01-17 11:00:00", Withdraw = 0, Deposit = 300.00m, Balance = 1050.00m, AccountName = "ACC003" },
                new Transaction { TransDate = "2024-01-18 16:00:00", Withdraw = 200.00m, Deposit = 0, Balance = 850.00m, AccountName = "ACC003" },
                new Transaction { TransDate = "2024-01-19 08:30:00", Withdraw = 0, Deposit = 1500.00m, Balance = 2350.00m, AccountName = "ACC003" },
                new Transaction { TransDate = "2024-01-15 12:00:00", Withdraw = 0, Deposit = 1500.00m, Balance = 1500.00m, AccountName = "ACC004" },
                new Transaction { TransDate = "2024-01-16 10:30:00", Withdraw = 400.00m, Deposit = 0, Balance = 1100.00m, AccountName = "ACC004" },
                new Transaction { TransDate = "2024-01-17 13:15:00", Withdraw = 0, Deposit = 600.00m, Balance = 1700.00m, AccountName = "ACC004" },
                new Transaction { TransDate = "2024-01-18 15:00:00", Withdraw = 250.00m, Deposit = 0, Balance = 1450.00m, AccountName = "ACC004" },
                new Transaction { TransDate = "2024-01-19 11:45:00", Withdraw = 0, Deposit = 800.00m, Balance = 2250.00m, AccountName = "ACC004" },
                new Transaction { TransDate = "2024-01-15 09:15:00", Withdraw = 0, Deposit = 3000.00m, Balance = 3000.00m, AccountName = "ACC005" },
                new Transaction { TransDate = "2024-01-16 15:30:00", Withdraw = 500.00m, Deposit = 0, Balance = 2500.00m, AccountName = "ACC005" },
                new Transaction { TransDate = "2024-01-17 12:45:00", Withdraw = 0, Deposit = 1000.00m, Balance = 3500.00m, AccountName = "ACC005" },
                new Transaction { TransDate = "2024-01-18 14:15:00", Withdraw = 750.00m, Deposit = 0, Balance = 2750.00m, AccountName = "ACC005" },
                new Transaction { TransDate = "2024-01-19 16:00:00", Withdraw = 0, Deposit = 2000.00m, Balance = 4750.00m, AccountName = "ACC005" }
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
