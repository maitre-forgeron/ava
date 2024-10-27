using Ava.Application.Queries.Therapists;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
    public class TherapistQueryHandler :
            IRequestHandler<GetAllTherapistsQuery, IEnumerable<Therapist>>,
            IRequestHandler<GetTherapistProfileQuery, Therapist>,
            IRequestHandler<GetTopReviewsForTherapistQuery, List<Review>>,
            IRequestHandler<GetMoreReviewsForTherapistQuery, List<Review>>
    {
        private readonly ITherapistRepository _therapistRepository;

        public TherapistQueryHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<IEnumerable<Therapist>> Handle(GetAllTherapistsQuery request, CancellationToken cancellationToken)
        {
            return await _therapistRepository.GetAllAsync();
        }

        public async Task<Therapist> Handle(GetTherapistProfileQuery request, CancellationToken cancellationToken)
        {
            return await _therapistRepository.GetTherapistByIdAsync(request.Id);
        }

        public async Task<List<Review>> Handle(GetTopReviewsForTherapistQuery request, CancellationToken cancellationToken)
        {
            return await _therapistRepository.GetTopReviewsForTherapistAsync(request.TherapistId, 3);
        }

        public async Task<List<Review>> Handle(GetMoreReviewsForTherapistQuery request, CancellationToken cancellationToken)
        {
            return await _therapistRepository.GetReviewsForTherapistAsync(request.TherapistId, request.Skip, request.Take);
        }
    }
}
