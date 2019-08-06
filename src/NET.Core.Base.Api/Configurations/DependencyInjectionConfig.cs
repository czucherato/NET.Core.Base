using NET.Core.Base.Data.Context;
using NET.Core.Base.Data.Repositories;
using NET.Core.Base.Business.Services;
using NET.Core.Base.Business.Interfaces;
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

            services.AddScoped<IFornecedorService, FornecedorService>();

            services.AddScoped<IEnderecoRep, EnderecoRep>();
            services.AddScoped<IFornecedorRep, FornecedorRep>();

            return services;
        }
    }
}
