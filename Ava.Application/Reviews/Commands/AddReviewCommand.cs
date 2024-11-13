using Ava.Application.Models;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Reviews.Commands;

public record AddReviewCommand(Guid AuthorId, Guid RecipientId, int Rating, string Summary) : IRequest<Result>;

public class AddReviewCommandHandler(AvaDbContext context) : IRequestHandler<AddReviewCommand, Result>
{
    private readonly AvaDbContext _context = context;

    public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Therapists.FindAsync([request.AuthorId], cancellationToken);
        if (author != null && request.AuthorId == request.RecipientId)
        {
            return Result.Failure(new Error("400", "A therapist cannot rate themselves."));
        }

        var existingReview = await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.AuthorId == request.AuthorId && r.RecipientId == request.RecipientId, cancellationToken);

        if (existingReview != null)
        {
            return Result.Failure(new Error("400", "The user has already rated this therapist."));
        }

        var review = new Review(Guid.NewGuid(), request.AuthorId, request.RecipientId, request.Rating, request.Summary);
        _context.Reviews.Add(review);
        
        var therapist = await _context.Therapists.FindAsync([request.RecipientId], cancellationToken);
        if (therapist == null)
        {
            return Result.Failure(new Error("400", "Therapist not found."));
        }

        var reviews = await _context.Reviews
            .Where(r => r.RecipientId == request.RecipientId)
            .ToListAsync(cancellationToken);

        var newAverageRating = (reviews.Sum(r => r.Rating) + request.Rating) / (reviews.Count + 1);
        therapist.UpdateRating(newAverageRating);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
