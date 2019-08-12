using System;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace NET.Core.Base.Api.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
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

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            return app;
        }
    }
}
