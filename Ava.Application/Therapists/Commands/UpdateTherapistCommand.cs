using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Therapists.Commands
{
    public class UpdateTherapistCommand : IRequest
    {
        public TherapistDto Therapist { get; set; }
    }

    public class UpdateTherapistCommandHandler : IRequestHandler<UpdateTherapistCommand>
    {
        private readonly ITherapistRepository _therapistRepository;

        public UpdateTherapistCommandHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task Handle(UpdateTherapistCommand request, CancellationToken cancellationToken)
        {
            var therapist = new Therapist(request.Therapist.UserProfileId, request.Therapist.Rating, request.Therapist.Summary, request.Therapist.CertificateId)
            {
                Reviews = request.Therapist.Reviews.Select(r => new Review
                {
                    SenderId = r.SenderId,
                    RecipientId = r.RecipientId,
                    ReviewValue = r.ReviewValue,
                    ReviewText = r.ReviewText
                }).ToList()
            };

            await _therapistRepository.UpdateTherapistAsync(therapist);
        }
    }
}
