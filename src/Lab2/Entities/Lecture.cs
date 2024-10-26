using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class Lecture : IEntity, IPrototype<Lecture>
{
    private readonly int _idCounter;

    public Lecture(string name, string description, string content, User author, int? parentId = null)
    {
        Id = ++_idCounter;
        Name = name;
        Description = description;
        Content = content;
        Author = author;
        ParentId = parentId;
    }

    public int Id { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public User Author { get; }

    public int? ParentId { get; private set; }

    public void Update(string name, string description, string content)
    {
        Name = name;
        Description = description;
        Content = content;
    }

    public Lecture Clone()
    {
        return new Lecture(Name, Description, Content, Author, Id);
    }
}