using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OndeAssisto.Common.Contracts;
using OndeAssisto.Web.App.Services;
using OndeAssisto.Web.App.Services.Notification;
using OndeAssisto.Web.App.Services.Search;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OndeAssisto.Web.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(Configuration["Api:BaseAddress"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR", false),
                };

                options.DefaultRequestCulture = new RequestCulture(supportedUICultures.FirstOrDefault());
                options.SupportedUICultures = supportedUICultures;
            });
            services.AddRazorPages().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            services.AddServerSideBlazor();
            services.AddApplicationInsightsTelemetry();
            services.AddOptions();
            services.AddAuthenticationCore();
            services.AddAuthorizationCore();
            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
