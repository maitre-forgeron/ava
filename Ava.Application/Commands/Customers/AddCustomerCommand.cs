using MediatR;
using Ava.Domain.Models.User;

namespace Ava.Application.Commands.Customers
{
    public class AddCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }

        public AddCustomerCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
