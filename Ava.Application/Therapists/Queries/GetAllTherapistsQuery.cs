using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries
{
    public class GetAllTherapistsQuery : IRequest<IEnumerable<TherapistDto>> { }

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
            var therapistDtos = therapists.Select(t => new TherapistDto
            {
                UserProfileId = t.UserProfileId,
                Rating = t.Rating,
                Summary = t.Summary,
                CertificateId = t.CertificateId,
                Reviews = t.Reviews.Select(r => new ReviewDto
                {
                    AuthorId = r.SenderId,
                    RecipientId = r.RecipientId,
                    Rating = r.ReviewValue,
                    Summary = r.ReviewText
                }).ToList()
            });

            return therapistDtos;
        }
    }
}
