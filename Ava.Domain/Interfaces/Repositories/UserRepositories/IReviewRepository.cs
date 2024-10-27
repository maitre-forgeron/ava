using Ava.Domain.Models.User;

namespace Ava.Domain.Interfaces.Repositories.UserRepositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetReviewsForRecipientAsync(Guid recipientId, int skip, int take);
        Task AddReviewAsync(Review review);
    }
}
