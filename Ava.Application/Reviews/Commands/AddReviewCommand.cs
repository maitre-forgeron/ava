using Ava.Domain.Models.User;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Reviews.Commands;

public record AddReviewCommand(Guid AuthorId, Guid RecipientId, int Rating, string Summary) : IRequest<Guid>;

public class AddReviewCommandHandler(AvaDbContext context) : IRequestHandler<AddReviewCommand, Guid>
{
    private readonly AvaDbContext _context = context;

    public async Task<Guid> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Therapists.FindAsync([request.AuthorId], cancellationToken);
        if (author != null && request.AuthorId == request.RecipientId)
        {
            throw new InvalidOperationException("A therapist cannot rate themselves.");
        }

        var existingReview = await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.AuthorId == request.AuthorId && r.RecipientId == request.RecipientId, cancellationToken);

        if (existingReview != null)
        {
            throw new InvalidOperationException("The user has already rated this therapist.");
        }

        var review = new Review(Guid.NewGuid(), request.AuthorId, request.RecipientId, request.Rating, request.Summary);
        _context.Reviews.Add(review);
        
        var therapist = await _context.Therapists.FindAsync([request.RecipientId], cancellationToken);
        if (therapist == null)
        {
            throw new KeyNotFoundException("Therapist not found.");
        }

        var reviews = await _context.Reviews
            .Where(r => r.RecipientId == request.RecipientId)
            .ToListAsync(cancellationToken);

        var newAverageRating = (reviews.Sum(r => r.Rating) + request.Rating) / (reviews.Count + 1);
        therapist.UpdateRating(newAverageRating);

        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}
