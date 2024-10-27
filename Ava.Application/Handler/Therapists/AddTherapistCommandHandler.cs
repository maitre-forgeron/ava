using Ava.Application.Commands.Therapists;
using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
    public class AddTherapistCommandHandler : IRequestHandler<AddTherapistCommand, TherapistDto>
    {
        private readonly ITherapistRepository _therapistRepository;

        public AddTherapistCommandHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<TherapistDto> Handle(AddTherapistCommand request, CancellationToken cancellationToken)
        {
            var therapist = new Therapist(
                userProfileId: request.TherapistDto.UserProfileId,
                rating: request.TherapistDto.Rating,
                summary: request.TherapistDto.Summary,
                certificateId: request.TherapistDto.CertificateId
            );

            if (request.TherapistDto.Reviews != null && request.TherapistDto.Reviews.Any())
            {
                foreach (var reviewDto in request.TherapistDto.Reviews)
                {
                    var review = new Review
                    {
                        SenderId = reviewDto.SenderId,
                        RecipientId = reviewDto.RecipientId,
                        ReviewValue = reviewDto.ReviewValue,
                        ReviewText = reviewDto.ReviewText,
                        Sender = null,
                        Recipient = null
                    };
                    therapist.Reviews.Add(review);
                }
            }

            await _therapistRepository.AddTherapistAsync(therapist);

            return new TherapistDto
            {
                UserProfileId = therapist.UserProfileId,
                Rating = therapist.Rating,
                Summary = therapist.Summary,
                CertificateId = therapist.CertificateId,
                Id = therapist.Id,
                Reviews = request.TherapistDto.Reviews
            };
        }
    }
}
