using BartsolutionsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BartsolutionsWebAPI.DB
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename = BartSolutionsDatabase.db");
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Name);

            modelBuilder.Entity<Incident>()
                .HasKey(i => i.Name);

            modelBuilder.Entity<Incident>()
                .HasMany<Account>(a => a.Accounts)
                .WithOne(a => a.Incident)
                .IsRequired(false);
        }
    }
}