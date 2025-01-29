using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class Subject : IEntity, IPrototype<Subject>
{
    private readonly int _idCounter;

    public Subject(string name, User author, GradingType gradingType, int totalPoints, IReadOnlyCollection<LabWork> labWorks, IReadOnlyCollection<Lecture> lectureMaterials, int? parentId = null)
    {
        Id = ++_idCounter;
        Name = name;
        Author = author;
        GradingType = gradingType;
        TotalPoints = totalPoints;
        ParentId = parentId;

        LabWorks = new List<LabWork>(labWorks);
        LectureMaterials = new List<Lecture>(lectureMaterials);
    }

    public int Id { get; }

    public string Name { get; private set; }

    public User Author { get; }

    public GradingType GradingType { get; private set; }

    public int TotalPoints { get; }

    public int? ParentId { get; private set; }

    public IReadOnlyCollection<LabWork> LabWorks { get; }

    public IReadOnlyCollection<Lecture> LectureMaterials { get; private set; }

    public Subject ShallowCopy()
    {
        var clonedSubject = (Subject)MemberwiseClone();
        clonedSubject.ParentId = Id;
        return clonedSubject;
    }

    public void Update(string name, GradingType gradingType, IReadOnlyCollection<Lecture> lectureMaterials)
    {
        Name = name;
        GradingType = gradingType;
        LectureMaterials = new List<Lecture>(lectureMaterials);
    }
}