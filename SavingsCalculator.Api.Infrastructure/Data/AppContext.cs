using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace SavingsCalculator.Api.Infrastructure.Data
{
    class AppContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppContext(DbContextOptions<IngageContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
