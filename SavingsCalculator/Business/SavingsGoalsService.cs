using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using SavingsCalculator.Api.Business.Models;
using SavingsCalculator.Api.Core;
using SavingsCalculator.Data;

namespace SavingsCalculator.Api.Business
{
    public class SavingsGoalsService : ISavingsGoalsService
    {
        public SavingsContext Context { get; set; }

        public SavingsGoalsService(SavingsContext context)
        { 
            Context = context;
        }

        public async Task<IList<SavingsGoal>> GetSavingsGoals(Guid userId)
        {
            var goals = this.Context.SavingsGoals.Where(g => g.UserId == userId).Select(g => new Business.Models.SavingsGoal()
            {
                Id = g.Id,
                UserId = g.UserId,
                Name = g.Name,
                CurrentAmount = g.CurrentAmount,
                TargetAmount = g.TargetAmount
            });

            return await goals.ToListAsync();
        }
    }
}
