using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Queries;

public record GetAllTherapistsQuery : IRequest<IEnumerable<TherapistDto>>;

public class GetAllTherapistsQueryHandler : IRequestHandler<GetAllTherapistsQuery, IEnumerable<TherapistDto>>
{
    private readonly AvaDbContext _context;

    public GetAllTherapistsQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TherapistDto>> Handle(GetAllTherapistsQuery request, CancellationToken cancellationToken)
    {
        //TODO paging needs to be done

        var therapistDtos = _context.Therapists
            .Select(t => new TherapistDto(
                t.Id,
                t.Rating,
                t.Summary,
                t.CertificateId,
                t.RecipientReviews
                    .Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary))
                    .ToList()))
            .AsNoTracking();

        return therapistDtos;
    }
}
