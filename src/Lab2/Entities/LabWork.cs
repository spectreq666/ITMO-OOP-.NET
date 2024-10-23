﻿using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class LabWork : IEntity, IPrototype<LabWork>
{
    private static int _idCounter = 0;

    public LabWork(string name, string description, string rateCriteria, int points, User author, int? parentId = null)
    {
        Id = ++_idCounter;
        Name = name;
        Description = description;
        RateCriteria = rateCriteria;
        Points = points;
        Author = author;
        ParentId = parentId;
    }

    public int? ParentId { get; private set; }

    public int Id { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string RateCriteria { get; private set; }

    public int Points { get; }

    public User Author { get; }

    public void Update(string name, string description, string rateCriteria)
    {
        Name = name;
        Description = description;
        RateCriteria = rateCriteria;
    }

    public LabWork Clone()
    {
        return new LabWork(Name, Description, RateCriteria, Points, Author, Id);
    }
}