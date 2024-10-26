namespace Ava.Domain.Models.Base;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    public DateTime CreateDate { get; protected set; }

    public DateTime? UpdateDate { get; protected set; }

    protected Entity()
    {
        CreateDate = DateTime.UtcNow;
    }

    protected Entity(Guid id) : this()
    {
        if (id == default)
        {
            throw new InvalidOperationException("Id is required");
        }
    }

    protected virtual void Update()
    {
        UpdateDate = DateTime.UtcNow;
    }
}
