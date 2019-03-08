using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SavingsCalculator.Api;
using SavingsCalculator.Api.Business.Models;

namespace SavingsCalculator.Api.Core
{
    public interface ISavingsGoalsService
    {
        Task<IList<Business.Models.SavingsGoal>> GetSavingsGoals(Guid userId);
        Task<SavingsGoal> AddSavingsGoal(Guid userId, string name, decimal currentAmount, decimal targetAmount);
        Task<SavingsGoal> UpdateSavingsGoal(Guid goalId, Guid userId, string name, decimal currentAmount, decimal targetAmount);
        Task<bool> DeleteSavingsGoal(Guid goalId);
    }
}
