using System;
using SavingsCalculator.Data.Entities;

namespace SavingsCalculator.Api.Data.Entities
{
    public class SavingsGoal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
