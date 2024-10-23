using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class LectureFactory : IBuilder<Lecture>
{
    private readonly int? _parentId;
    private string _name;
    private string _description;
    private string _content;
    private User? _author;

    public LectureFactory()
    {
        _name = string.Empty;
        _description = string.Empty;
        _content = string.Empty;
        _parentId = null;
        _author = null;
    }

    public LectureFactory WithName(string name)
    {
        _name = name;
        return this;
    }

    public LectureFactory WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public LectureFactory WithContent(string content)
    {
        _content = content;
        return this;
    }

    public LectureFactory WithAuthor(User user)
    {
        _author = user;
        return this;
    }

    public Lecture Build()
    {
        if (_author is null)
        {
            throw new ArgumentException("Author must be specified.");
        }

        return new Lecture(_name, _description, _content, _author, _parentId);
    }
}