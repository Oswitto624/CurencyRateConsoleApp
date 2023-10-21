using Domain.Entities.Base.Interfaces;

namespace Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; } = null!;
}
