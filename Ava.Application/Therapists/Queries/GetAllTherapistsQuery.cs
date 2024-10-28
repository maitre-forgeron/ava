using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries;

public record GetAllTherapistsQuery : IRequest<IEnumerable<TherapistDto>>;

public class GetAllTherapistsQueryHandler : IRequestHandler<GetAllTherapistsQuery, IEnumerable<TherapistDto>>
{
    private readonly ITherapistRepository _therapistRepository;

    public GetAllTherapistsQueryHandler(ITherapistRepository therapistRepository)
    {
        _therapistRepository = therapistRepository;
    }

    public async Task<IEnumerable<TherapistDto>> Handle(GetAllTherapistsQuery request, CancellationToken cancellationToken)
    {
        var therapists = await _therapistRepository.GetAllAsync();

        var therapistDtos = therapists
            .Select(t => new TherapistDto(
                t.Id,
                t.Rating,
                t.Summary,
                t.CertificateId,
                t.RecipientReviews
                    .Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary))
                    .ToList()));

        return therapistDtos;
    }
}
