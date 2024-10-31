using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface ISubjectBuilder
{
    ISubjectBuilder WithName(string name);

    ISubjectBuilder WithAuthor(User author);

    ISubjectBuilder WithGradingType(GradingFormat gradingFormat, int specifiedPoints);

    ISubjectBuilder WithTotalPoints(int totalPoints);

    ISubjectBuilder WithLabWorks(IReadOnlyCollection<LabWork> labWorks);

    ISubjectBuilder WithLectureMaterials(IReadOnlyCollection<Lecture> lectureMaterials);

    Subject Build();
}