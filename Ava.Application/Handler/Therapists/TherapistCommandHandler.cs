using Ava.Application.Commands.Therapists;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
    public class TherapistCommandHandler :
            IRequestHandler<AddTherapistCommand>,
            IRequestHandler<UpdateTherapistCommand>,
            IRequestHandler<DeleteTherapistCommand>
    {
        private readonly ITherapistRepository _therapistRepository;

        public TherapistCommandHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task Handle(AddTherapistCommand request, CancellationToken cancellationToken)
        {
            await _therapistRepository.AddTherapistAsync(request.Therapist);
        }

        public async Task Handle(UpdateTherapistCommand request, CancellationToken cancellationToken)
        {
            await _therapistRepository.UpdateTherapistAsync(request.Therapist);
        }

        public async Task Handle(DeleteTherapistCommand request, CancellationToken cancellationToken)
        {
            await _therapistRepository.DeleteTherapistAsync(request.Id);
        }
    }
}
