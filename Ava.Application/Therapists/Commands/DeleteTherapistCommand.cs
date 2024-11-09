using Ava.Application.Models;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Commands;

public record DeleteTherapistCommand(Guid Id) : IRequest<Result>;

public class DeleteTherapistCommandHandler : IRequestHandler<DeleteTherapistCommand, Result>
{
    private readonly AvaDbContext _context;

    public DeleteTherapistCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteTherapistCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Therapists.Where(t => t.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}
