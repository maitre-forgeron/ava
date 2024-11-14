using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Db;
using MediatR;

namespace Ava.Application.Therapists.Commands;

public record AddTherapistCommand(CreateTherapistDto Dto) : IRequest<Result>;

public class AddTherapistCommandHandler : IRequestHandler<AddTherapistCommand, Result>
{
    private readonly AvaDbContext _context;

    public AddTherapistCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddTherapistCommand request, CancellationToken cancellationToken)
    {
        //TODO certificate id
        var therapist = new Therapist(request.Dto.Id, request.Dto.FirstName, request.Dto.LastName, request.Dto.PersonalId, request.Dto.Rating, request.Dto.Summary, Guid.NewGuid());

        _context.Add(therapist);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
