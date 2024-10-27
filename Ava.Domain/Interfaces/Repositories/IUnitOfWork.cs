using Ava.Domain.Interfaces.Repositories.UserRepositories;

namespace Ava.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    //Repository properties here
    ICustomerRepository Customers { get; }

    ITherapistRepository Therapists { get; }
    
    IReviewRepository Reviews { get; }

    Task<int> CommitAsync();
}
