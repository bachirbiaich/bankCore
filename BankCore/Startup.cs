using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BankCore.Models;

namespace BankCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                                    // Indique si l'éditeur validera lors de la validation du token
                                    ValidateIssuer = true,
                                    // chaîne représentant l'éditeur
                                    ValidIssuer = AuthOptions.ISSUER,

                                    // le consommateur du token sera-t-il validé?
                                    ValidateAudience = true,
                                    // installation de jeton utilisateur
                                    ValidAudience = AuthOptions.AUDIENCE,
                                    // la vie sera-t-elle validée
                                    ValidateLifetime = true,

                                    // installation de clé de sécurité
                                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                                    // validation de la clé de sécurité
                                    ValidateIssuerSigningKey = true,
                    };
                });
            services.AddCors();
            services.AddMvc();

           services.AddDbContext<BankCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BankCoreContext")));  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();  // Auth call
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
            app.UseMvc();
        }
    }
}
