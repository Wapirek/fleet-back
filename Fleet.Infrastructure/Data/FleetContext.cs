using Fleet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure.Data
{
    public class FleetContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<CatalogEntity> Catalogs { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<UserProfileEntity> UserProfile { get; set; }
        public DbSet<IncomeEntity> Incomes { get; set; }
        public DbSet<OutcomeEntity> Outcomes { get; set; }

        public FleetContext(DbContextOptions<FleetContext> options) : base(options)
        {
            
        }
    }
}