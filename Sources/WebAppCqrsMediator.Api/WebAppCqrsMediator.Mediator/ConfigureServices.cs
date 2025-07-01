using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAppCqrsMediator.Mediator.Common.Behaviours;

namespace WebAppCqrsMediator.Mediator
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMediatorService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(c => {
                c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                });
            return services;
        }
    }
}
