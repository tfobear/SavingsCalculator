using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingsCalculator.Api.Business;
using SavingsCalculator.Api.Core;

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
    }
}