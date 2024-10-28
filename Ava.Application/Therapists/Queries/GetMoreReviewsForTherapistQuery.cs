using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries
{
    public class GetMoreReviewsForTherapistQuery : IRequest<List<ReviewDto>>
    {
        public Guid TherapistId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

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
            return reviews.Select(r => new ReviewDto
            {
                SenderId = r.AuthorId,
                RecipientId = r.RecipientId,
                ReviewValue = r.Rating,
                ReviewText = r.Summary
            }).ToList();
        }
    }
}
