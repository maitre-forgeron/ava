using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries;

public record GetTherapistProfileQuery(Guid Id) : IRequest<TherapistDto>;

public class GetTherapistProfileQueryHandler : IRequestHandler<GetTherapistProfileQuery, TherapistDto>
{
    private readonly ITherapistRepository _therapistRepository;

    public GetTherapistProfileQueryHandler(ITherapistRepository therapistRepository)
    {
        _therapistRepository = therapistRepository;
    }

    public async Task<TherapistDto> Handle(GetTherapistProfileQuery request, CancellationToken cancellationToken)
    {
        var therapist = await _therapistRepository.GetTherapistByIdAsync(request.Id);

        return new TherapistDto(
            therapist.Id,
            therapist.Rating,
            therapist.Summary,
            therapist.CertificateId,
            therapist.RecipientReviews?.Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary)).ToList());
    }
}
