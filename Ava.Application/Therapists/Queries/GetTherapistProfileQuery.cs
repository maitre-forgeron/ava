using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Queries;

public record GetTherapistProfileQuery(Guid Id) : IRequest<TherapistDto>;

public class GetTherapistProfileQueryHandler : IRequestHandler<GetTherapistProfileQuery, TherapistDto>
{
    private readonly AvaDbContext _context;

    public GetTherapistProfileQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<TherapistDto> Handle(GetTherapistProfileQuery request, CancellationToken cancellationToken)
    {
        var therapist = await _context.Therapists
            .Where(t => t.Id == request.Id)
            .Select(t => new TherapistDto(
                t.Id,
                t.FirstName,
                t.LastName,
                t.Rating,
                t.Summary,
                t.CertificateId,
                t.RecipientReviews
                    .OrderByDescending(r => r.Rating)
                    .Take(3)
                    .Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary)).ToList() ?? new List<ReviewDto>()))
            .AsNoTracking()
            .SingleOrDefaultAsync();


        return therapist;
    }
}
