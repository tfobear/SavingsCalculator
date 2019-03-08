using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SavingsCalculator.Api;

namespace SavingsCalculator.Api.Core
{
    public interface ISavingsGoalsService
    {
        Task<IList<Business.Models.SavingsGoal>> GetSavingsGoals(Guid userId);
        Task<Business.Models.SavingsGoal> AddSavingsGoal(string name, decimal currentAmount, decimal targetAmount, Guid userId);
        Task<bool> DeleteSavingsGoal(Guid goalId);
    }
}
