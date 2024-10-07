namespace Ava.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

    void Add(T entity);

    void DeleteRange(IEnumerable<T> records);

    void Delete(T record);

    Task DeleteAsync(Guid id);

    IQueryable<T?> GetQueryable(Expression<Func<T, bool>> predicate, bool tracking, params Expression<Func<T, object>>[] includes);
}
