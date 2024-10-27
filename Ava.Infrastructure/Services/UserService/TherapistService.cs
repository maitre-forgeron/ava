using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Services.UserService.Interfaces;

namespace Ava.Infrastructure.Services.UserService
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _therapistRepository;

        public TherapistService(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<IEnumerable<Therapist>> GetAllTherapistsAsync()
        {
            return await _therapistRepository.GetAllAsync();
        }

        public async Task<Therapist> GetTherapistProfileAsync(Guid id)
        {
            return await _therapistRepository.GetTherapistByIdAsync(id);
        }

        public async Task AddTherapistAsync(Therapist therapist)
        {
            await _therapistRepository.AddTherapistAsync(therapist);
        }

        public async Task UpdateTherapistAsync(Therapist therapist)
        {
            await _therapistRepository.UpdateTherapistAsync(therapist);
        }

        public async Task DeleteTherapistAsync(Guid id)
        {
            await _therapistRepository.DeleteTherapistAsync(id);
        }

        public async Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId)
        {
            return await _therapistRepository.GetTopReviewsForTherapistAsync(therapistId, 3);
        }

        public async Task<List<Review>> GetMoreReviewsForTherapistAsync(Guid therapistId, int skip, int take)
        {
            return await _therapistRepository.GetReviewsForTherapistAsync(therapistId, skip, take);
        }
    }
}
