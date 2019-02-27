using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavingsCalculator.Data.Entities;
using System;

namespace SavingsCalculator.Data
{
    public class SavingsContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public SavingsContext(DbContextOptions<SavingsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
