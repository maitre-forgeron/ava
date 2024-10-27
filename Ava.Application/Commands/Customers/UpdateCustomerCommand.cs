using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public CustomerDto CustomerDto { get; set; }

        public UpdateCustomerCommand(CustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }
}
