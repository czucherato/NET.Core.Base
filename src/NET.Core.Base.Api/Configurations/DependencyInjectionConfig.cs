using Microsoft.AspNetCore.Http;
using NET.Core.Base.Data.Context;
using Microsoft.Extensions.Options;
using NET.Core.Base.Api.Extensions;
using NET.Core.Base.Data.Repositories;
using NET.Core.Base.Business.Services;
using NET.Core.Base.Business.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using NET.Core.Base.Business.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace NET.Core.Base.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<NetCoreBaseContext>();
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfig>();

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IEnderecoRep, EnderecoRep>();
            services.AddScoped<IFornecedorRep, FornecedorRep>();
            services.AddScoped<IProdutoRep, ProdutoRep>();

            return services;
        }
    }
}
