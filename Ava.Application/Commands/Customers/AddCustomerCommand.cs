using MediatR;
using Ava.Domain.Models.User;
using Ava.Application.Dtos;

namespace Ava.Application.Commands.Customers
{
    public class AddCustomerCommand : IRequest<Customer>
    {
        public CustomerDto CustomerDto { get; set; }

        public AddCustomerCommand(CustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }
}
