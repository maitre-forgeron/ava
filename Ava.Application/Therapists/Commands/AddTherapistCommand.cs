using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Therapists.Commands;

public record AddTherapistCommand(CreateTherapistDto Dto) : IRequest<Guid>;

public class AddTherapistCommandHandler : IRequestHandler<AddTherapistCommand, Guid>
{
    private readonly ITherapistRepository _therapistRepository;

    public AddTherapistCommandHandler(ITherapistRepository therapistRepository)
    {
        _therapistRepository = therapistRepository;
    }

    public async Task<Guid> Handle(AddTherapistCommand request, CancellationToken cancellationToken)
    {
        //TODO certificate id
        var therapist = new Therapist(request.Dto.Id, request.Dto.FirstName, request.Dto.LastName, request.Dto.PersonalId, request.Dto.Rating, request.Dto.Summary, Guid.NewGuid());

        await _therapistRepository.AddTherapistAsync(therapist);

        return therapist.Id;
    }
}
