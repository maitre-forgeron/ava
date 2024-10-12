using Ava.Application.ForTest.Commands;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ava.Application
{
    public static class Configuration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ForTestCommand))!));

            return services;
        }
    }
}
