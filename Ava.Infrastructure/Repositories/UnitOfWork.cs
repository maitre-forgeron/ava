namespace Ava.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AvaDbContext _context;

    //TODO: inject needed repositories
    public UnitOfWork(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
}
