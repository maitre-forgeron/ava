using Ava.Domain.Interfaces.Repositories.UserRepositories;

namespace Ava.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AvaDbContext _context;

    public ICustomerRepository Customers { get; }
    public ITherapistRepository Therapists { get; }
    public IReviewRepository Reviews { get; }

    //TODO: inject needed repositories
    public UnitOfWork(
        AvaDbContext context,
        ICustomerRepository customerRepository,
        ITherapistRepository therapistRepository,
        IReviewRepository reviewRepository)
    {
        _context = context;
        Customers = customerRepository;
        Therapists = therapistRepository;
        Reviews = reviewRepository;
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
}
