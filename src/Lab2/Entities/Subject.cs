using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class Subject : IEntity, IPrototype<Subject>
{
    private static int _idCounter = 0;
    private readonly List<LabWork> _labWorks;
    private List<Lecture> _lectureMaterials;

    public Subject(string name, User author, GradingType gradingType, int totalPoints, IReadOnlyCollection<LabWork> labWorks, IReadOnlyCollection<Lecture> lectureMaterials, int? parentId = null)
    {
        Id = ++_idCounter;
        Name = name;
        Author = author;
        GradingType = gradingType;
        TotalPoints = totalPoints;
        ParentId = parentId;

        _labWorks = new List<LabWork>(labWorks);
        _lectureMaterials = new List<Lecture>(lectureMaterials);
    }

    public int Id { get; }

    public string Name { get; private set; }

    public User Author { get; }

    public GradingType GradingType { get; private set; }

    public int TotalPoints { get; }

    public int? ParentId { get; }

    public Subject Clone()
    {
        return new Subject(Name, Author, GradingType, TotalPoints, _labWorks, _lectureMaterials, Id);
    }

    public void Update(string name, GradingType gradingType, IReadOnlyCollection<Lecture> lectureMaterials)
    {
        Name = name;
        GradingType = gradingType;
        _lectureMaterials = new List<Lecture>(lectureMaterials);
    }
}