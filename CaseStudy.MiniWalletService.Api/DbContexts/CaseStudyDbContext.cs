using CaseStudy.MiniWalletService.Api.Models.Entities;
using CaseStudy.MiniWalletService.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CaseStudy.Data.DbContexts
{
    public class CaseStudyDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Wallet.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = 1,
                    UserName = "Test1",
                    Money = new Dictionary<CurrencyType, decimal>
                    {
                        {CurrencyType.TRY, 20}
                    }
                },
                new Wallet
                {
                    Id = 2,
                    UserName = "Test3",
                    Money = new Dictionary<CurrencyType, decimal>
                    {
                        {CurrencyType.USD, 10}
                    }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
