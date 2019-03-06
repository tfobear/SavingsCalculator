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
    }
}
