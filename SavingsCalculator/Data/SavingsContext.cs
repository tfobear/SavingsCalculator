using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavingsCalculator.Api.Data.Entities;
using SavingsCalculator.Data.Entities;
using System;

namespace SavingsCalculator.Data
{
    public class SavingsContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<SavingsGoal> SavingsGoals { get; set; }

        public SavingsContext(DbContextOptions<SavingsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(SavingsContext).Assembly);
        }
    }
}
