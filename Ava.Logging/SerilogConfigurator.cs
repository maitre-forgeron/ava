using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

using System.Diagnostics.CodeAnalysis;

namespace Ava.Logging
{
    [ExcludeFromCodeCoverage]
    public static class SerilogConfigurator
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
        {
            var connectionString = context.Configuration.GetConnectionString("AvaConnectionString");

            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true 
            };

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: sinkOptions)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);
        };

    }
}
