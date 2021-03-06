﻿using AutoMapper;
using NET.Core.Base.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NET.Core.Base.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using NET.Core.Base.Api.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace NET.Core.Base.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostingEnvironment.IsDevelopment()) builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.WebApiConfig();
            services.AddSwaggerConfig();
            services.ResolveDependencies();
            services.AddAutoMapper(typeof(Startup));
            services.AddIdentityConfiguration(Configuration);
            services.AddDbContext<NetCoreBaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddLoggingConfiguration(Configuration);
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                app.UseHsts();
                app.UseCors("Production");
            }

            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvcConfiguration();
            app.UseSwaggerConfig(provider);
            app.UseLoggingConfiguration();
        }
    }
}
