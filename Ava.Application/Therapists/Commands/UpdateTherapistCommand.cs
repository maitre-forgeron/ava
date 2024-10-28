using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Commands;

public record UpdateTherapistCommand(UpdateTherapistDto Dto) : IRequest;

public class UpdateTherapistCommandHandler : IRequestHandler<UpdateTherapistCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ITherapistRepository _therapistRepository;

    public UpdateTherapistCommandHandler(IUnitOfWork uow, ITherapistRepository therapistRepository)
    {
        _uow = uow;
        _therapistRepository = therapistRepository;
    }

    public async Task Handle(UpdateTherapistCommand request, CancellationToken cancellationToken)
    {
        var therapist = await _therapistRepository.GetByIdAsync(request.Dto.Id);

        if (therapist == null)
        {
            throw new InvalidOperationException();
        }

        therapist.Update(request.Dto.FirstName, request.Dto.LastName, request.Dto.Rating, request.Dto.Summary);

        await _uow.CommitAsync();
    }
}
