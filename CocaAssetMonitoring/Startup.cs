using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using CocaAssetMonitoring.Services;
using CocaAssetMonitoring.Services.Hangfire;
using Microsoft.OpenApi.Models;
using CocaAssetMonitoring.Persistence.Context;
using CocaAssetMonitoring.Domain.IScheduler;
using CocaAssetMonitoring.Models;
using CocaAssetMonitoring.Scheduler.Jobs;
using CocaAssetMonitoring.Domain.IServices;
using CocaAssetMonitoring.Scheduler;
using CocaAssetMonitoring.Domain.IJobs;

namespace CocaAssetMonitoring
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Geniethings Dashboard API", Version = "v1", });
            });
            services.AddDbContext<ApplicationDbContext>(p => p.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<BrokerDbContext>(p => p.UseSqlServer(Configuration.GetConnectionString("BrokerDefault")));
            services.AddDbContext<InterfaceDbContext>(p => p.UseSqlServer(Configuration.GetConnectionString("InterfaceDefault")));
            services.Configure<IISServerOptions>(o =>
            {
                o.AllowSynchronousIO = true;
            });
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireDefault"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                }));
            services.AddHttpClient();
            services.AddHangfireServer();

            services.AddScoped<IMachinePerformanceJob, MachinePerformanceJob>();
            services.AddScoped<IMachinePerformanceService, MachinePerformanceService>();

            services.AddScoped<IMachineProcessJob, MachineProcessJob>();
            services.AddScoped<IMachineProcessService, MachineProcessService>();

            services.AddScoped<IStateJob,StateJob>();
            services.AddScoped<IStateService,StateService>();
            
            services.AddScoped<IMCProcessJob,MCProcessJob>();
            services.AddScoped<IMCProcessService,MCProcessService>();

            services.AddScoped<IJobsScheduler, JobsScheduler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IJobsScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            scheduler.ScheduleRecurringJobs();

            app.UseStaticFiles();
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "GenieThings Dashboard API");
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilter() }
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
