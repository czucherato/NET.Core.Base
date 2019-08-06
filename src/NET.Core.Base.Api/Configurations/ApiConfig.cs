using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NET.Core.Base.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            //services.AddCors(options => options.AddPolicy("Development", builder => builder.AllowAnyMethod().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseHttpsRedirection();
            //app.UseCors("Development");

            return app;
        }
    }
}
