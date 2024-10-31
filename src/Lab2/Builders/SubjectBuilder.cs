using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class SubjectBuilder : ISubjectBuilder
{
    private readonly int? _parentId;
    private int _totalPoints;
    private string _name;
    private User? _author;
    private GradingType? _gradingType;
    private IReadOnlyCollection<LabWork> _labWorks;
    private IReadOnlyCollection<Lecture> _lectureMaterials;

    public SubjectBuilder()
    {
        _name = string.Empty;
        _author = null;
        _gradingType = null;
        _totalPoints = 0;
        _labWorks = new List<LabWork>();
        _lectureMaterials = new List<Lecture>();
        _parentId = null;
    }

    public ISubjectBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ISubjectBuilder WithAuthor(User author)
    {
        _author = author;
        return this;
    }

    public ISubjectBuilder WithGradingType(GradingFormat gradingFormat, int specifiedPoints)
    {
        _gradingType = new GradingType(gradingFormat, specifiedPoints);
        return this;
    }

    public ISubjectBuilder WithTotalPoints(int totalPoints)
    {
        _totalPoints = totalPoints;
        return this;
    }

    public ISubjectBuilder WithLabWorks(IReadOnlyCollection<LabWork> labWorks)
    {
        _labWorks = new List<LabWork>(labWorks);
        return this;
    }

    public ISubjectBuilder WithLectureMaterials(IReadOnlyCollection<Lecture> lectureMaterials)
    {
        _lectureMaterials = new List<Lecture>(lectureMaterials);
        return this;
    }

    public Subject Build()
    {
        if (_author is null)
        {
            throw new ArgumentException("Author must be specified.");
        }

        if (_gradingType is null)
        {
            throw new ArgumentException("Grading type must be specified.");
        }

        if (_totalPoints != 100)
        {
            throw new ArgumentException("Total points must be 100.");
        }

        return new Subject(_name, _author, _gradingType, _totalPoints, _labWorks, _lectureMaterials, _parentId);
    }
}