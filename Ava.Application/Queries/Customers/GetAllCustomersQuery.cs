using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Customers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
    }
}
