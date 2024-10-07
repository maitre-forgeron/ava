namespace Ava.Domain.Models.Base;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot()
    {

    }

    protected AggregateRoot(Guid id) : base(id)
    {

    }
}
