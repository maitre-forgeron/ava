using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Customers.Commands
{
    public class AddCustomerCommand : IRequest<Customer>
    {
        public CustomerDto CustomerDto { get; set; }

        public AddCustomerCommand(CustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }

    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                UserProfileId = request.CustomerDto.UserProfileId
            };

            await _customerRepository.AddCustomerAsync(customer);
            return customer;
        }
    }
}