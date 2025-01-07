using Microsoft.Extensions.DependencyInjection;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Infrastructure.Repository;

namespace WhiteLagoon.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IVillaRepository, VillaRepository>();

            return services;
        }
    }
}
