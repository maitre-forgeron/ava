using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public Customer Customer { get; set; }

        public UpdateCustomerCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
