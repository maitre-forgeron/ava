using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Queries;

public record GetCustomerProfileQuery(Guid Id) : IRequest<CustomerDto>;

public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, CustomerDto?>
{
    private readonly AvaDbContext _context;

    public GetCustomerProfileQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto?> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(c => c.Id == request.Id)
            .Select(c => new CustomerDto(c.Id, c.FirstName, c.LastName, c.PersonalId, c.PhotoId))
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);
    }
}
