﻿namespace Domain.Base.Interfaces;

public interface INamedEntity : IEntity
{
    string Name { get; set; }
}
