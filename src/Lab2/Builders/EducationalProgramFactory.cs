using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class EducationalProgramFactory : IBuilder<EducationalProgram>
{
    private readonly List<(int Semester, Subject Subject)> _subjects;
    private string _name;
    private User? _programLeader;

    public EducationalProgramFactory()
    {
        _name = string.Empty;
        _programLeader = null;
        _subjects = new List<(int Semester, Subject Subject)>();
    }

    public EducationalProgramFactory WithName(string name)
    {
        _name = name;
        return this;
    }

    public EducationalProgramFactory WithProgramLeader(User programLeader)
    {
        _programLeader = programLeader;
        return this;
    }

    public EducationalProgramFactory WithSubject(int semester, Subject subject)
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
