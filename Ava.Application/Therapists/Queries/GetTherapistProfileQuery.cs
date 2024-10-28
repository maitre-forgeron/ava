using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries
{
    public class GetTherapistProfileQuery : IRequest<TherapistDto>
    {
        public Guid Id { get; set; }
    }

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
            return new TherapistDto
            {
                Id = request.Id,
                UserProfileId = therapist.UserProfileId,
                Rating = therapist.Rating,
                Summary = therapist.Summary,
                CertificateId = therapist.CertificateId,
                Reviews = therapist.RecipientReviews.Select(r => new ReviewDto
                {
                    AuthorId = r.AuthorId,
                    RecipientId = r.RecipientId,
                    Rating = r.Rating,
                    Summary = r.Summary
                }).ToList()
            };
        }
    }
}
