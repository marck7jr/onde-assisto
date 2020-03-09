using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OndeAssisto.Common.Contracts.Jwt;
using OndeAssisto.Web.Api.Data;
using OndeAssisto.Web.Api.Services.Jwt;
using System;

namespace OndeAssisto.Web.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (Environment.IsDevelopment() && false)
                {
                    options.UseInMemoryDatabase("OndeAssistoDatabase");
                }
                else
                {
                    var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("OndeAssistoDatabase"))
                    {
                        Password = Configuration["OndeAssistoDatabase:Password"]
                    };

                    options.UseSqlServer(builder.ConnectionString);
                    options.EnableSensitiveDataLogging();
                }
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var settings = new JwtServiceSettings(Configuration);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = settings.SecurityKey,
                    ValidAudience = settings.Audience,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IJwtServiceSettings, JwtServiceSettings>();
            services.AddApplicationInsightsTelemetry();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
