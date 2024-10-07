namespace Ava.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AvaDbContext _context;

    public Repository(AvaDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T record)
    {
        _context.Set<T>().Remove(record);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
        {
            throw new InvalidOperationException($"{typeof(T).Name} with id: {id} does not exist");
        }

        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> records)
    {
        _context.Set<T>().RemoveRange(records);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var queryable = GetQueryable(null, false, includes);

        return await queryable.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        var queryable = GetQueryable(x => x.Id == id, true, includes);

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate, bool tracking, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> queryable = predicate == null ? _context.Set<T>() : _context.Set<T>().Where(predicate);

        queryable = tracking ? queryable : queryable.AsNoTracking();

        foreach (var include in includes)
        {
            queryable = queryable.Include(include);
        }

        return queryable;
    }
}
