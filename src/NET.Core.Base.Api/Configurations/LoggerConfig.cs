using System;
using Elmah.Io.AspNetCore;
using HealthChecks.UI.Client;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using NET.Core.Base.Api.Extensions;
using Elmah.Io.AspNetCore.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace NET.Core.Base.Api.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "34a67e2be81a4b0bb60257ca7d45cb69";
                o.LogId = new Guid("9af9342c-4ca5-44c0-8791-add4ab6e9a84");
            });

            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "34a67e2be81a4b0bb60257ca7d45cb69";
            //        o.LogId = new Guid("9af9342c-4ca5-44c0-8791-add4ab6e9a84");
            //    });

            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            services.AddHealthChecks()
                .AddElmahIoPublisher("34a67e2be81a4b0bb60257ca7d45cb69", new Guid("9af9342c-4ca5-44c0-8791-add4ab6e9a84"), "NET.Core.Base")
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSql")
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")));

            services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options => options.UIPath = "/hc-ui");
            return app;
        }
    }
}
