namespace Ava.Tests.Unit
{
    [CollectionDefinition("Booking_Collection")]
    public class BookingCollectionFixture : ICollectionFixture<ServiceProviderFixture>
    {

    }

    [CollectionDefinition("Customer_Collection")]
    public class CustomerCollectionFixture : ICollectionFixture<ServiceProviderFixture>
    {

    }
    
    [CollectionDefinition("Therapist_Collection")]
    public class TherapistCollectionFixture : ICollectionFixture<ServiceProviderFixture>
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
