using System;
using Microsoft.EntityFrameworkCore;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.DAL
{
    public class PasswordContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Environment.GetEnvironmentVariable("connectionString");

            if (string.IsNullOrEmpty(connString))
            {
                throw new Exception("Connection string not present, use specify 'connectionString' environment variable");
            }

            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PasswordRule>().HasMany(x => x.PasswordRuleConfigurations).WithOne(x => x.Rule);
        }
        
        public DbSet<PasswordRule> PasswordRules { get; set; }
        public DbSet<PasswordRuleConfiguration> PasswordRuleConfigurations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}