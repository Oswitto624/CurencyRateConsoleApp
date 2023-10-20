using Domain.Base.Interfaces;

namespace Domain.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; } = null!;
}
