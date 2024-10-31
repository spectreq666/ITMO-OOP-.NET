using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class EducationalProgram : IEntity
{
    private readonly int _idCounter;
    private readonly List<(int Semester, Subject Subject)> _subjects;

    public EducationalProgram(string name, User programLeader, IReadOnlyCollection<(int Semester, Subject Subject)> subjects)
    {
        Id = ++_idCounter;
        Name = name;
        Leader = programLeader;
        _subjects = new List<(int Semester, Subject Subject)>(subjects);
    }

    public int Id { get; }

    public string Name { get; }

    public User Leader { get; }

    public IReadOnlyCollection<(int Semester, Subject Subject)> Subjects => _subjects.AsReadOnly();
}