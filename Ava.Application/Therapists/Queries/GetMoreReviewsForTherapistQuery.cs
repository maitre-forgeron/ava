using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries;

public record GetMoreReviewsForTherapistQuery(Guid TherapistId, int Skip, int Take) : IRequest<List<ReviewDto>>;

public class GetMoreReviewsForTherapistQueryHandler : IRequestHandler<GetMoreReviewsForTherapistQuery, List<ReviewDto>>
{
    private readonly ITherapistRepository _therapistRepository;

    public GetMoreReviewsForTherapistQueryHandler(ITherapistRepository therapistRepository)
    {
        _therapistRepository = therapistRepository;
    }

    public async Task<List<ReviewDto>> Handle(GetMoreReviewsForTherapistQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _therapistRepository.GetReviewsForTherapistAsync(request.TherapistId, request.Skip, request.Take);

        return reviews.Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary)).ToList();
    }
}
