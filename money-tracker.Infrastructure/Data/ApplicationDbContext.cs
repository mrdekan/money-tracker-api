using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Entities;

namespace money_tracker.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Jar> Jars { get; set; }
        public DbSet<CurrencyBalance> CurrencyBalances { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<IdentityRole<int>>().ToTable("roles");

            modelBuilder.Entity<Jar>().ToTable("jars");

            modelBuilder.Entity<CurrencyBalance>().ToTable("currency_balances");

            modelBuilder.Entity<Store>().ToTable("stores");

            modelBuilder.Entity<Transaction>().ToTable("transactions");

            modelBuilder.Entity<Jar>()
                .HasOne(j => j.User)
                .WithMany(u => u.Jars)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Jar)
                .WithMany(j => j.Stores)
                .HasForeignKey(s => s.JarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Source)
                .WithMany(cb => cb.Transactions)
                .HasForeignKey(t => t.SourceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CurrencyBalance>()
                .HasOne(cb => cb.User)
                .WithMany(u => u.CurrencyBalances)
                .HasForeignKey(cb => cb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CurrencyBalance>()
                .HasOne(cb => cb.Store)
                .WithMany(u => u.CurrencyBalances)
                .HasForeignKey(cb => cb.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Jar>()
               .HasOne(j => j.TargetCurrency)
               .WithMany(c => c.TargetedJars)
               .HasForeignKey(j => j.TargetCurrencyId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CurrencyBalance>()
               .HasOne(cb => cb.Currency)
               .WithMany(c => c.Balances)
               .HasForeignKey(cb => cb.CurrencyId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
