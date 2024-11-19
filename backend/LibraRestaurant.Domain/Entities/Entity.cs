using System;

namespace LibraRestaurant.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public int NumberId { get; private set; }
    public bool Deleted { get; private set; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Entity()
    {
        
    }

    protected Entity(int numberId)
    {
        NumberId = numberId;
    }

    public void SetId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException($"{nameof(id)} may not be empty");
        }

        Id = id;
    }

    public void SetNumberId(int numberId)
    {
        NumberId = numberId;
    }

    public void Delete()
    {
        Deleted = true;
    }

    public void Undelete()
    {
        Deleted = false;
    }
}