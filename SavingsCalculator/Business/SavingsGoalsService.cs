﻿using System;
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

        public async Task<SavingsGoal> AddSavingsGoal(string name, decimal currentAmount, decimal targetAmount, Guid userId)
        {
            var dataGoal = new Data.Entities.SavingsGoal()
            {
                Name = name,
                CurrentAmount = currentAmount,
                TargetAmount = targetAmount,
                UserId = userId
            };

            // TODO: should wrap in own repository
            Context.SavingsGoals.Add(dataGoal);
            await Context.SaveChangesAsync();

            return new SavingsGoal
            {
                Id = dataGoal.Id,
                UserId = dataGoal.UserId,
                Name = dataGoal.Name,
                CurrentAmount = dataGoal.CurrentAmount,
                TargetAmount = dataGoal.TargetAmount
            };
        }

        public async Task<bool> DeleteSavingsGoal(Guid goalId)
        {
            Data.Entities.SavingsGoal goal = new Data.Entities.SavingsGoal() {Id = goalId};

            Context.SavingsGoals.Attach(goal);
            Context.SavingsGoals.Remove(goal);

            var count = await Context.SaveChangesAsync();

            return count > 0;
        }
    }
}
