using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SavingsCalculator.Api.Common;
using SavingsCalculator.Data;
using SavingsCalculator.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SavingsCalculator.Api.Data
{
    public class AppSeeder
    {
        private readonly SavingsContext context;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AppSeeder(
            SavingsContext ctx,
            IHostingEnvironment hosting,
            UserManager<AppUser> userManager,
            IConfiguration config)
        {
            this.context = ctx;
            this.hosting = hosting;
            this.userManager = userManager;
            this.config = config;
        }

        public async Task SeedAsync()
        {
            context.Database.EnsureCreated();

            // Create Seed Users
            var path = Path.Combine(hosting.ContentRootPath, "Data/DefaultUsers.json");
            var json = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<IEnumerable<AppUser>>(json);

            foreach (var user in users)
            {
                var found = await userManager.FindByEmailAsync(user.Email);

                if (found == null)
                {
                    user.Id = GuidHelper.GetDeterministicGuid(user.FirstName + user.LastName);
                    user.UserName = user.Email;

                    var result = await userManager.CreateAsync(user, config["Seed:DefaultPassword"]);

                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Couldn't create new user in AppSeeder");
                    }
                }
            }
        }
    }
}
