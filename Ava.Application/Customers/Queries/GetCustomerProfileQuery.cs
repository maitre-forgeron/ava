using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Customers.Queries
{
    public class GetCustomerProfileQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }

        public GetCustomerProfileQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerProfileQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(request.Id);
        }
    }
}
