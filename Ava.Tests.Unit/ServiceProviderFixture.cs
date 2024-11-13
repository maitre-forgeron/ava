namespace Ava.Tests.Unit
{
    [CollectionDefinition("Booking_Collection")]
    public class BookingCollectionFixture : ICollectionFixture<ServiceProviderFixture>
    {

    }

    public class ServiceProviderFixture
    {
        public IServiceProvider ServiceProvider { get; }
        public ServiceProviderFixture()
        {
            ServiceProvider = TestStartUp.ConfigureServices();
        }
    }
}
