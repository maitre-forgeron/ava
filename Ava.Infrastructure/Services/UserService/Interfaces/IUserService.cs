using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Task<Customer> GetCustomerProfileAsync(Guid customerId);
        Task<Therapist> GetTherapistProfileAsync(Guid therapistId);
        Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId, int count);
        Task<List<Review>> GetMoreReviewsForTherapistAsync(Guid therapistId, int skip, int take);
        Task AddReviewAsync(Guid senderId, Guid recipientId, int reviewValue, string reviewText);
    }
}
