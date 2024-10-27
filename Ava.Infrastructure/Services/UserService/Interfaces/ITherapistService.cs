using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Services.UserService.Interfaces
{
    public interface ITherapistService
    {
        Task<IEnumerable<Therapist>> GetAllTherapistsAsync();
        Task<Therapist> GetTherapistProfileAsync(Guid id);
        Task AddTherapistAsync(Therapist therapist);
        Task UpdateTherapistAsync(Therapist therapist);
        Task DeleteTherapistAsync(Guid id);

        Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId);
        Task<List<Review>> GetMoreReviewsForTherapistAsync(Guid therapistId, int skip, int take);
    }
}
