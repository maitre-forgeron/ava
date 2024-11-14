using Microsoft.Extensions.DependencyInjection;

namespace Ava.Tests.Unit
{
    public class TestStartUp
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Add services here

            return services.BuildServiceProvider();
        }
    }
}
