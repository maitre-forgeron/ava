using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Queries.Customers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }
}
