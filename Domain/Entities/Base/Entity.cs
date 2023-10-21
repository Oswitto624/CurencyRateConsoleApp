using Domain.Entities.Base.Interfaces;

namespace Domain.Entities.Base;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
