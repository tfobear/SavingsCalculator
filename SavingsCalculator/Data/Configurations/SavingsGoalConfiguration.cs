using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SavingsCalculator.Api.Data.Entities;

namespace SavingsCalculator.Api.Data.EntityTypeConfigurations
{
    public class SavingsGoalConfiguration : IEntityTypeConfiguration<SavingsGoal>
    {
        public void Configure(EntityTypeBuilder<SavingsGoal> builder)
        {
            builder.Property(s => s.CurrentAmount).HasColumnType("decimal(19,4)");
            builder.Property(s => s.TargetAmount).HasColumnType("decimal(19,4)");
        }
    }
}
