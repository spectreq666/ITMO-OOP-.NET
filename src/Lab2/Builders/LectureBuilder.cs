using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class LectureBuilder : ILectureBuilder
{
    private readonly int? _parentId;
    private string _name;
    private string _description;
    private string _content;
    private User? _author;

    public LectureBuilder()
    {
        _name = string.Empty;
        _description = string.Empty;
        _content = string.Empty;
        _parentId = null;
        _author = null;
    }

    public ILectureBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ILectureBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ILectureBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public ILectureBuilder WithAuthor(User author)
    {
        _author = author;
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