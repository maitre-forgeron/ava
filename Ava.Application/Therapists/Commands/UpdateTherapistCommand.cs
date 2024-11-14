using Ava.Application.Constants;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Commands;

public record UpdateTherapistCommand(UpdateTherapistDto Dto) : IRequest<Result>;

public class UpdateTherapistCommandHandler : IRequestHandler<UpdateTherapistCommand, Result>
{
    private readonly AvaDbContext _context;

    public UpdateTherapistCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateTherapistCommand request, CancellationToken cancellationToken)
    {
        var therapist = await _context.Therapists.SingleOrDefaultAsync(t => t.Id == request.Dto.Id, cancellationToken);

        if (therapist == null)
        {
            return Result.Failure(TherapistErrors.NotFound);
        }

        therapist.Update(request.Dto.FirstName, request.Dto.LastName, request.Dto.Rating, request.Dto.Summary);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
