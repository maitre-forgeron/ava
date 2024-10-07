namespace Ava.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    //Repository properties here

    Task<int> CommitAsync();
}
