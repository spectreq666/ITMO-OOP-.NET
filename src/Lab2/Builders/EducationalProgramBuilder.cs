using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class EducationalProgramBuilder : IEducationalProgramBuilder
{
    private readonly List<(int Semester, Subject Subject)> _subjects;
    private string _name;
    private User? _programLeader;

    public EducationalProgramBuilder()
    {
        _name = string.Empty;
        _programLeader = null;
        _subjects = new List<(int Semester, Subject Subject)>();
    }

    public IEducationalProgramBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IEducationalProgramBuilder WithLeader(User programLeader)
    {
        _programLeader = programLeader;
        return this;
    }

    public IEducationalProgramBuilder WithSubject(int semester, Subject subject)
    {
        _subjects.Add((semester, subject));
        return this;
    }

    public EducationalProgram Build()
    {
        if (string.IsNullOrEmpty(_name))
        {
            throw new ArgumentException("Program name must be specified.");
        }

        if (_programLeader is null)
        {
            throw new ArgumentException("Program leader must be specified.");
        }

        return new EducationalProgram(_name, _programLeader, _subjects);
    }
}