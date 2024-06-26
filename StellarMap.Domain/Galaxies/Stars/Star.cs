﻿namespace StellarMap.Domain.Galaxies.Stars;

public class Star(StarId id, Name name, StarClassification classification, StarSize size) : Entity<StarId>(id)
{
    public Name Name { get; } = name;
    public StarClassification Classification { get; } = classification;
    public StarSize Size { get; } = size;
}