using System;

namespace SavingsCalculator.Api.Business.Models
{
    public class SavingsGoal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
