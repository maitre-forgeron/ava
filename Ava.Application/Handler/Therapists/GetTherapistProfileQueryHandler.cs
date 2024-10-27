using Ava.Application.Dtos;
using Ava.Application.Queries.Therapists;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
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
                Reviews = therapist.Reviews.Select(r => new ReviewDto
                {
                    SenderId = r.SenderId,
                    RecipientId = r.RecipientId,
                    ReviewValue = r.ReviewValue,
                    ReviewText = r.ReviewText
                }).ToList()
            };
        }
    }
}
