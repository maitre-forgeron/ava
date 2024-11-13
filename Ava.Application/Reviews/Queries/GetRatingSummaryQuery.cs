using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Reviews.Queries;

public record GetRatingSummaryQuery(Guid TherapistId) : IRequest<RatingSummary>;

public class GetRatingSummaryQueryHandler : IRequestHandler<GetRatingSummaryQuery, RatingSummary>
{
    private readonly AvaDbContext _context;

    public GetRatingSummaryQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<RatingSummary> Handle(GetRatingSummaryQuery request, CancellationToken cancellationToken)
    {
        var averageRating = await _context.Therapists
            .Where(t => t.Id == request.TherapistId)
            .SelectMany(t => t.RecipientReviews)
            .Select(r => (double?)r.Rating)
            .AverageAsync(cancellationToken: cancellationToken) ?? 0;

        var totalReviews = await _context.Therapists
            .Where(t => t.Id == request.TherapistId)
            .SelectMany(t => t.RecipientReviews)
            .CountAsync(cancellationToken);

        return new RatingSummary(averageRating, totalReviews);
    }
}