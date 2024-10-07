namespace Ava.Domain.Models.Base;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    public DateTime CreateDate { get; protected set; }

    public DateTime? UpdateDate { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(Guid id)
    {
        if (id == default)
        {
            throw new InvalidOperationException("Id is required");
        }

        CreateDate = DateTime.UtcNow;
    }

    protected virtual void Update()
    {
        UpdateDate = DateTime.UtcNow;
    }
}
