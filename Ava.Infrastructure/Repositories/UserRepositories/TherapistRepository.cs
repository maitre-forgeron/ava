using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Repositories.UserRepositories
{
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(AvaDbContext context) : base(context) { }

        public async Task<Therapist?> GetTherapistByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task AddTherapistAsync(Therapist therapist)
        {
            Add(therapist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTherapistAsync(Guid id)
        {
            var therapist = await GetTherapistByIdAsync(id);
            if (therapist != null)
            {
                Delete(therapist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId, int count)
        {
            return await GetQueryable(x => x.Id == therapistId, false, x => x.RecipientReviews)
                         .SelectMany(t => t.RecipientReviews)
                         .OrderByDescending(r => r.Rating)
                         .Take(count)
                         .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsForTherapistAsync(Guid therapistId, int skip, int take)
        {
            return await GetQueryable(x => x.Id == therapistId, false, x => x.RecipientReviews)
                         .SelectMany(t => t.RecipientReviews)
                         .OrderByDescending(r => r.Rating)
                         .Skip(skip)
                         .Take(take)
                         .ToListAsync();
        }
    }
}
