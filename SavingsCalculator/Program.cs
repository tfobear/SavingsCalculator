using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavingsCalculator.Api.Data;

namespace SavingsCalculator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // Seed DB before hosting
            RunSeeding(host);

            // Run WebHost
            host.Run();
        }

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<AppSeeder>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupAppConfig)
                .UseStartup<Startup>();

        private static void SetupAppConfig(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            // removes defaults
            builder.Sources.Clear();

            builder.AddJsonFile("./config.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}
