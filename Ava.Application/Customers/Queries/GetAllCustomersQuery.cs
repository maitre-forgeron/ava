using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Queries;

public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>;

public class GetAllCustomerssQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly AvaDbContext _context;

    public GetAllCustomerssQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        //TODO paging needs to be done

        var customerDtos = _context.Customers
            .Select(t => new CustomerDto(
                t.Id,
                t.FirstName,
                t.LastName,
                t.PersonalId,
                t.PhotoId))
            .AsNoTracking();

        return customerDtos;
    }
}
