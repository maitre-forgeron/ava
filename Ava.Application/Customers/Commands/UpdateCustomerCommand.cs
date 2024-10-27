using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public CustomerDto CustomerDto { get; set; }

        public UpdateCustomerCommand(CustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                UserProfileId = request.CustomerDto.UserProfileId
            };

            await _customerRepository.UpdateCustomerAsync(customer);
            return Unit.Value;
        }
    }
}
