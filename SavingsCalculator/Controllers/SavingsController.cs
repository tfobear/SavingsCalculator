using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingsCalculator.Api.Business;
using SavingsCalculator.Api.Core;
using SavingsCalculator.Api.Data.ViewModels;

namespace SavingsCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private readonly ISavingsGoalsService _savingsGoalService;

        public SavingsController(ISavingsGoalsService savingsGoalService)
        {
            _savingsGoalService = savingsGoalService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetSavingsGoals()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var goals = await _savingsGoalService.GetSavingsGoals(userId);

            return Ok(goals);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddSavingsGoal(SavingsGoalViewModel goal)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var added = await _savingsGoalService.AddSavingsGoal(userId, goal.Name, goal.CurrentAmount, goal.TargetAmount);

            if (added != null)
            {
                return Ok(added);
            }

            return StatusCode(500, "Nothing was added.");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateSavingsGoal(Guid goalId, [FromBody] SavingsGoalViewModel goal)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updated = await _savingsGoalService.UpdateSavingsGoal(goalId, userId, goal.Name, goal.CurrentAmount, goal.TargetAmount);

            if (updated != null)
            {
                return Ok(updated);
            }

            return StatusCode(500, "Nothing was updated.");
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteSavingsGoal(Guid goalId)
        {
            //TODO: ensure this user is deleting it?
            var deleted = await _savingsGoalService.DeleteSavingsGoal(goalId);

            if (deleted)
            {
                return Ok();
            }

            return StatusCode(500, "Nothing was deleted.");
        }
    }
}