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
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace GlueHome.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Filter.ByExcluding(le => ShouldExcludeFromLog(le))
                    .WriteTo.Console(new JsonFormatter())
                    .Enrich.FromLogContext()
                    .CreateLogger();
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

            services.AddSingleton<IMysqlContext>(new MysqlContext());

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

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
