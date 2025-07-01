using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppCqrsMediator.Domain.Repositories;
using WebAppCqrsMediator.Infrastructure.Reposytories;

namespace WebAppCqrsMediator.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INodeRepositoty, NodeRepositoty>();
            services.AddTransient<ISecureExceptionModelRepository, SecureExceptionModelRepository>();
            return services;
        }

    }
}
