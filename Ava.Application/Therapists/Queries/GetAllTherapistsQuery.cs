using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Queries;

public record GetAllTherapistsQuery(Guid? CategoryId, int Skip, int Take) : IRequest<IEnumerable<TherapistDto>>;

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
        var category = _context.Categories.FirstOrDefault();

        var therapistDtos = _context.Therapists
            .Where(t => !request.CategoryId.HasValue || t.TherapistCategories.Any(c => c.CategoryId == request.CategoryId))
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(t => new TherapistDto(
                t.Id,
                t.Rating,
                t.Summary,
                t.CertificateId,
                t.RecipientReviews.Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary)).ToList(),
                t.TherapistCategories.Select(c => new CategoryDto(c.CategoryId, c.Category.Name))
                    .ToList()))
            .AsNoTracking();

        return therapistDtos;
    }
}
