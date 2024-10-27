using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Services.UserService.Interfaces;

namespace Ava.Infrastructure.Services.UserService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerProfileAsync(Guid id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
