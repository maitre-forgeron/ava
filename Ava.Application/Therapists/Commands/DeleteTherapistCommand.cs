using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Therapists.Commands;

public record DeleteTherapistCommand(Guid Id) : IRequest;

public class DeleteTherapistCommandHandler : IRequestHandler<DeleteTherapistCommand>
{
    private readonly AvaDbContext _context;

    public DeleteTherapistCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTherapistCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Therapists.Where(t => t.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
    }
}
