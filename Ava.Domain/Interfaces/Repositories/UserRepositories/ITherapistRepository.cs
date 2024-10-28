using Ava.Domain.Models.User;

namespace Ava.Domain.Interfaces.Repositories.UserRepositories
{
    public interface ITherapistRepository : IRepository<Therapist>
    {
        Task<Therapist?> GetTherapistByIdAsync(Guid id);
        Task AddTherapistAsync(Therapist therapist);
        Task DeleteTherapistAsync(Guid id);

        Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId, int count);
        Task<List<Review>> GetReviewsForTherapistAsync(Guid therapistId, int skip, int take);   
    }
}
