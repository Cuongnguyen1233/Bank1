using Microsoft.EntityFrameworkCore;
using Bank1.Models;

namespace Bank1.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Account entity
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountName);
                entity.Property(e => e.AccountName).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Pincode).IsRequired();
            });

            // Configure Transaction entity
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.TransDate).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Withdraw).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.Deposit).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.Balance).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.AccountName).HasMaxLength(20).IsRequired();
            });
        }
    }
}
