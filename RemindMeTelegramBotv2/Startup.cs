using System.Collections.Specialized;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2
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
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IRemindService,RemindService>();
            services.AddSingleton(provider => GetScheduler());
            services.AddSingleton(typeof(IDbRepository<>), typeof(DbRepository<>));
            services.AddSingleton<IDbContext,DbContext>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IBotClient,BotClient>();
            services.AddScoped<IGetInfoService,GetInfoService>();
            services.AddScoped<IUpdateService, UpdateService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRemindService remindService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            remindService.InitializeTimers();
            app.UseRouting();
            app.UseCors();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IScheduler GetScheduler()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "RemindsScheduler",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = "3",
                ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
            };
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start();
            return scheduler;
        }
    }
}
