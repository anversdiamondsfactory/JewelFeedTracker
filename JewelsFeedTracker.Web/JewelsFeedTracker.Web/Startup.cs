using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using JewelsFeedTracker.Data.Access;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using JewelsFeedTracker.Data.Models.Models;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using Serilog.Events;
using System.Data.SqlClient;
using WebApiWithSwagger.ErrorHandler;
using WebApiWithSwagger.HangfireJob;
using JewelsFeedTracker.FactoryManager;
using Hangfire.Storage;
using JewelsFeedTracker.Data.Access.QueryProcessor;
using JewelsFeedTracker.Utility;
using System.Net;
using JewelsFeedTracker.Utility.RowDataManager;

namespace WebApiWithSwagger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string _connectionString = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()

                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));


            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddRouting();
            services.AddControllers();
            // Register Swagger
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample API", Version = "version 1" });

           });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            try
            {
                if (env.EnvironmentName == Microsoft.Extensions.Hosting.Environments.Development)
                {
                    app.UseDeveloperExceptionPage();
                }

                // exception handling in entire application
                app.ConfigureCustomExceptionMiddleware();

                app.UseRouting();
                app.UseAuthentication();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllers();
                    endpoints.MapHangfireDashboard();

                });
                // Hangfire job dashboard integration 
                app.UseHangfireDashboard();

                Log.Information("note-------------Starting hangfire jobs");
                Initialize(serviceProvider);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Hangfire Jobs exception ----" + ex.Message);
            }
        }
        private void Initialize(IServiceProvider serviceProvider)
        {
            //DataBaseHelper.OpenConection();
            HangFireScheduler.FeedJobScheduler(Configuration.GetSection("CronJobDuration").Value);
            //DataBaseHelper.CloseConnection();
        }

    }
}
