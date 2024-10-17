using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

namespace Ava.Logging
{
    public static class Configuration
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddTransient<LoggingDelegatingHandler>();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });

            return services;
        }
    }
}
