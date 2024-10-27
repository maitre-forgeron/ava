using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Customers
{
    public class GetCustomerProfileQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }

        public GetCustomerProfileQuery(Guid id)
        {
            Id = id;
        }
    }
}
