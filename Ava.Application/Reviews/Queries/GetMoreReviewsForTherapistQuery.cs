using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Reviews.Queries;

public record GetMoreReviewsForTherapistQuery(Guid TherapistId, int Skip, int Take) : IRequest<List<ReviewDto>>;

public class GetMoreReviewsForTherapistQueryHandler : IRequestHandler<GetMoreReviewsForTherapistQuery, List<ReviewDto>>
{
    private readonly AvaDbContext _context;

    public GetMoreReviewsForTherapistQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewDto>> Handle(GetMoreReviewsForTherapistQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Therapists
            .Where(t => t.Id == request.TherapistId)
            .SelectMany(t => t.RecipientReviews)
            .Where(r => r.RecipientId == request.TherapistId)
            .Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return reviews;
    }
}
