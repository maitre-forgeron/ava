using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Reviews.Commands
{
    public record UpdateReviewCommand(Guid AuthorId, Guid RecipientId, int NewRating, string NewSummary) : IRequest<Guid>;

    public class UpdateReviewCommandHandler(AvaDbContext context) : IRequestHandler<UpdateReviewCommand, Guid>
    {
        private readonly AvaDbContext _context = context;

        public async Task<Guid> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.AuthorId == request.AuthorId && r.RecipientId == request.RecipientId, cancellationToken) 
                ?? throw new InvalidOperationException("Review not found. Please add a review first.");

            if (request.AuthorId == request.RecipientId)
            {
                throw new InvalidOperationException("A therapist cannot rate themselves.");
            }

            existingReview.Update(request.NewRating, request.NewSummary);

            var therapist = await _context.Therapists.FindAsync([request.RecipientId], cancellationToken) 
                ?? throw new KeyNotFoundException("Therapist not found.");
            
            var reviews = await _context.Reviews
                .Where(r => r.RecipientId == request.RecipientId)
                .ToListAsync(cancellationToken);

            var newAverageRating = reviews.Average(r => r.Rating);
            therapist.UpdateRating(newAverageRating);

            await _context.SaveChangesAsync(cancellationToken);

            return existingReview.Id;
        }
    }
}
