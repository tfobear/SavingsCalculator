using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SavingsCalculator.Api.Data.ViewModels;
using SavingsCalculator.Data.Entities;
using SavingsCalculator.Data.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SavingsCalculator.Controllers
{
    /// <summary>
    /// Api for User Accounts
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration config;

        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                return await Login(login.Email, login.Password);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel register)
        {
            var found = await userManager.FindByEmailAsync(register.Email);

            if (found == null)
            {
                if (register.Password == register.ConfirmPassword)
                {
                    var newUser = new AppUser
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        UserName = register.Email
                    };


                    var result = await userManager.CreateAsync(newUser, register.Password);

                    if (result.Succeeded)
                    {
                        return await Login(register.Email, register.Password);
                    }
                    else
                    {
                        var error = result.Errors.First();
                        return BadRequest(new { message = error.Description, code = error.Code });
                    }
                }

                return BadRequest(new { message = "Passwords do not match" });
            }

            return BadRequest(new { message = "User already exists with email" });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool IsAuthorized()
        {
            return true;
        }

        // checks password and creates the token
        private async Task<IActionResult> Login(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(
                    user, password, false);

                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        config["Tokens:Issuer"],
                        config["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: creds
                    );

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiresIn = token.ValidTo
                    };

                    return Created("", results);
                }
            }

            return BadRequest();
        }

    }
}
