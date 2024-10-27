using Ava.Application.Commands.Therapists;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
    public class DeleteTherapistCommandHandler : IRequestHandler<DeleteTherapistCommand>
    {
        private readonly ITherapistRepository _therapistRepository;

        public DeleteTherapistCommandHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task Handle(DeleteTherapistCommand request, CancellationToken cancellationToken)
        {
            await _therapistRepository.DeleteTherapistAsync(request.Id);
        }
    }
}
