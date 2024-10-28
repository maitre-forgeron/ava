using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;

namespace Ava.Infrastructure.Repositories.UserRepositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AvaDbContext context) : base(context) { }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await DeleteAsync(id);
            await _context.SaveChangesAsync();
        }
    }
}
