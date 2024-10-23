using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class LabWorkFactory : IBuilder<LabWork>
{
    private readonly int? _parentId;
    private string _name;
    private string _description;
    private string _rateCriteria;
    private int _points;
    private User? _author;

    public LabWorkFactory()
    {
        _name = string.Empty;
        _description = string.Empty;
        _rateCriteria = string.Empty;
        _points = 0;
        _parentId = null;
        _author = null;
    }

    public LabWorkFactory WithName(string name)
    {
        _name = name;
        return this;
    }

    public LabWorkFactory WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public LabWorkFactory WithRateCriteria(string rateCriteria)
    {
        _rateCriteria = rateCriteria;
        return this;
    }

    public LabWorkFactory WithPoints(int points)
    {
        _points = points;
        return this;
    }

    public LabWorkFactory WithAuthor(User user)
    {
        _author = user;
        return this;
    }

    public LabWork Build()
    {
        if (_author is null)
        {
            throw new ArgumentException("Author must be specified.");
        }

        if (_points is < 0 or > 100)
        {
            throw new ArgumentException("Points must be between 0 and 100.");
        }

        return new LabWork(_name, _description, _rateCriteria, _points, _author, _parentId);
    }
}