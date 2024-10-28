using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Customers.Commands;

public record UpdateCustomerCommand(UpdateCustomerDto Dto) : IRequest<Unit>;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(IUnitOfWork uow, ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Dto.Id);

        if (customer == null)
        {
            throw new InvalidOperationException();    
        }

        customer.Update(request.Dto.FirstName, request.Dto.LastName);

        await _uow.CommitAsync();

        return Unit.Value;
    }
}
