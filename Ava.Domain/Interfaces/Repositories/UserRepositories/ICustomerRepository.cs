using Ava.Domain.Models.User;

namespace Ava.Domain.Interfaces.Repositories.UserRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
    }
}
