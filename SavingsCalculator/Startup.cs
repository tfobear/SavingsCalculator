using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SavingsCalculator.Api.Data;
using SavingsCalculator.Data;
using SavingsCalculator.Data.Entities;
using Swashbuckle.AspNetCore.Swagger;
using VueCliMiddleware;

namespace SavingsCalculator.Api
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly IHostingEnvironment environment;

        public Startup(IConfiguration config, IHostingEnvironment environment)
        {
            this.config = config;
            this.environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "SavingsCalculator.xml")
                );
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Savings Calculator",
                    Description = "A really COOL savings app",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Tom Fobear",
                        Email = "tfobear@gmail.com",
                        Url = "https://github.com/tfobear/"
                    }
                });
            });

            services.AddDbContext<SavingsContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("DefaultConnectionString"));
            });

            services.AddIdentity<AppUser, AppRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireUppercase = false;
                cfg.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
            })
            .AddEntityFrameworkStores<SavingsContext>();

            services.AddTransient<AppSeeder>();

            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = config["Tokens:Issuer"],
                        ValidAudience = config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
                    };
                });

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "ClientApp/dist/";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Savings Calculator API V1");
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
#if DEBUG
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "dev", port: 8080); // optional port
                }
#endif
            });
        }
    }
}
