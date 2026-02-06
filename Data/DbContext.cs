using Microsoft.EntityFrameworkCore;
using FinanceApi.Models;

namespace FinanceApi.Data
{
    public class AppDbContext : DbContext
    {
        //Configurações do banco
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Cria tabelas
        public DbSet<Models.Transaction> Transactions { get; set; }
        //Ajusta o tipo da coluna de valor para não causar problemas nas contas monetárias.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Value)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}