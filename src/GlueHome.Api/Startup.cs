using System;
using System.Linq;
using GlueHome.Api.Authentication;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;
using GlueHome.Api.Repositories;
using GlueHome.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace GlueHome.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public EnvironmentSettings EnvironmentSettings { get; private set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Filter.ByExcluding(le => ShouldExcludeFromLog(le))
                    .WriteTo.Console(new JsonFormatter())
                    .Enrich.FromLogContext()
                    .CreateLogger();

            this.EnvironmentSettings = new EnvironmentSettings(configuration);
        }

        public bool ShouldExcludeFromLog(LogEvent le)
        {
            if (le.Level >= LogEventLevel.Warning)
            {
                return false;
            }
            var prefixesToRemove = new string[] { "Microsoft.AspNetCore", };
            LogEventPropertyValue value;

            var hasSourceContext = le.Properties.TryGetValue("SourceContext", out value);
            if (hasSourceContext)
            {
                var trimmed = value.ToString().Trim('\"');
                return prefixesToRemove.Any(p => trimmed.StartsWith(p, StringComparison.CurrentCultureIgnoreCase));
            }

            return false;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlueHome API", Version = "v1" });
            });

            services.AddSingleton<IMysqlContext>(
                c => new MysqlContext(
                    c.GetService<ILogger<MysqlContext>>(), 
                    EnvironmentSettings.MysqlConnectionString));

            services.AddScoped<IRepository<Auth>, AuthRepository>();
            services.AddScoped<IRepository<Member>, MemberRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();

            services.AddTransient<IAuthenticator, AuthService>();
            services.AddTransient<IDeliveryReader, DeliveryService>();
            services.AddTransient<IDeliveryWriter, DeliveryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLogger(loggerFactory.CreateLogger("Request"));
            app.AddUserAuthentication(
                loggerFactory.CreateLogger("Authentication"), 
                app.ApplicationServices.GetService<IAuthenticator>());

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlueHome API v1");
            });
        }
    }
}
