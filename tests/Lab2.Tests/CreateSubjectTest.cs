using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Factories;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Xunit;

namespace Lab2.Tests;

public class CreateSubjectTest
{
    [Fact]
    public void BuildSubject_WhenTotalPointsIsNot100_Failed()
    {
        var author = new User("Author 1");
        var entityFactory = new EntityFactory(author);

        var labWorks = new List<LabWork>
        {
            entityFactory.CreateLabWork()
                .WithName("Lab 1")
                .WithDescription("Description for Lab 1")
                .WithRateCriteria("Rate Criteria")
                .WithPoints(50)
                .Build(),
        };

        var lectureMaterials = new List<Lecture>
        {
            entityFactory.CreateLecture()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content for Lecture 1")
                .Build(),
        };

        ISubjectBuilder subjectBuilder = entityFactory.CreateSubject()
            .WithName("Invalid Subject")
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(50)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials);

        Assert.Throws<ArgumentException>(() => subjectBuilder.Build());
    }

    [Fact]
    public void BuildSubject_WithCorrectData_Success()
    {
        var author = new User("Author 1");
        var entityFactory = new EntityFactory(author);

        var labWorks = new List<LabWork>
        {
            entityFactory.CreateLabWork()
                .WithName("Lab 1")
                .WithDescription("Description for Lab 1")
                .WithRateCriteria("Rate Criteria")
                .WithPoints(50)
                .Build(),
        };

        var lectureMaterials = new List<Lecture>
        {
            entityFactory.CreateLecture()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content for Lecture 1")
                .Build(),
        };

        ISubjectBuilder subject = entityFactory.CreateSubject()
            .WithName("Valid Subject")
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials);

        Exception? exception = Record.Exception(() => subject.Build());

        Assert.Null(exception);
    }
}
