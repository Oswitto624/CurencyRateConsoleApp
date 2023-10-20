using Domain.Base.Interfaces;

namespace Domain.Base;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
