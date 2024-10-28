using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries;

public record GetTopReviewsForTherapistQuery(Guid TherapistId) : IRequest<List<ReviewDto>>;

public class GetTopReviewsForTherapistQueryHandler : IRequestHandler<GetTopReviewsForTherapistQuery, List<ReviewDto>>
{
    private readonly ITherapistRepository _therapistRepository;

    public GetTopReviewsForTherapistQueryHandler(ITherapistRepository therapistRepository)
    {
        _therapistRepository = therapistRepository;
    }

    public async Task<List<ReviewDto>> Handle(GetTopReviewsForTherapistQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _therapistRepository.GetTopReviewsForTherapistAsync(request.TherapistId, 3);

        return reviews.Select(r => new ReviewDto(r.Id, r.AuthorId, r.RecipientId, r.Rating, r.Summary)).ToList();
    }
}
