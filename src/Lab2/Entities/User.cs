using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class User : IEntity
{
    private static int _idCounter = 0;

    public User(string name)
    {
        Id = ++_idCounter;
        Name = name;
    }

    public int Id { get; }

    public string Name { get; }
}