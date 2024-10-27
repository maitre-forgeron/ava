using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteCustomerAsync(request.Id);
            return Unit.Value;
        }
    }
}
