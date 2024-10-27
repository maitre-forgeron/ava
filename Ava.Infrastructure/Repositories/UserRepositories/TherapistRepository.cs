using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Repositories.UserRepositories
{
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(AvaDbContext context) : base(context) { }

        public async Task<Therapist> GetTherapistByIdAsync(Guid id)
        {
            return await GetByIdAsync(id, x => x.UserProfile, x => x.Reviews);
        }

        public async Task AddTherapistAsync(Therapist therapist)
        {
            Add(therapist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTherapistAsync(Therapist therapist)
        {
            _context.Entry(therapist).State = EntityState.Modified;
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
            return await GetQueryable(x => x.Id == therapistId, false, x => x.Reviews)
                         .SelectMany(t => t.Reviews)
                         .OrderByDescending(r => r.ReviewValue)
                         .Take(count)
                         .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsForTherapistAsync(Guid therapistId, int skip, int take)
        {
            return await GetQueryable(x => x.Id == therapistId, false, x => x.Reviews)
                         .SelectMany(t => t.Reviews)
                         .OrderByDescending(r => r.ReviewValue)
                         .Skip(skip)
                         .Take(take)
                         .ToListAsync();
        }
    }
}
