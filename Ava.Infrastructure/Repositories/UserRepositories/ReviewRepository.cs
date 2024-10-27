using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Repositories.UserRepositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AvaDbContext context) : base(context) { }

        public async Task<List<Review>> GetReviewsForRecipientAsync(Guid recipientId, int skip, int take)
        {
            return await GetQueryable(r => r.RecipientId == recipientId, false)
                         .OrderByDescending(r => r.ReviewValue)
                         .Skip(skip)
                         .Take(take)
                         .ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
