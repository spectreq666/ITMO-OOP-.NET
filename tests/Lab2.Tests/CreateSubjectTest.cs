using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Xunit;

namespace Lab2.Tests;

public class CreateSubjectTest
{
    [Fact]
    public void BuildSubject_WhenTotalPointsIsNot100_Failed()
    {
        var author = new User("Author 1");

        var labWorks = new List<LabWork>
        {
            new LabWorkFactory()
                .WithName("Lab 1")
                .WithDescription("Description for Lab 1")
                .WithRateCriteria("Rate Criteria")
                .WithPoints(50)
                .WithAuthor(author)
                .Build(),
        };

        var lectureMaterials = new List<Lecture>
        {
            new LectureFactory()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content for Lecture 1")
                .WithAuthor(author)
                .Build(),
        };

        SubjectFactory subjectFactory = new SubjectFactory()
            .WithName("Invalid Subject")
            .WithAuthor(author)
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(50)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials);

        Assert.Throws<ArgumentException>(() => subjectFactory.Build());
    }

    [Fact]
    public void BuildSubject_WithCorrectData_Success()
    {
        var author = new User("Author 1");

        var labWorks = new List<LabWork>
        {
            new LabWorkFactory()
                .WithName("Lab 1")
                .WithDescription("Description for Lab 1")
                .WithRateCriteria("Rate Criteria")
                .WithPoints(50)
                .WithAuthor(author)
                .Build(),
        };

        var lectureMaterials = new List<Lecture>
        {
            new LectureFactory()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content for Lecture 1")
                .WithAuthor(author)
                .Build(),
        };

        SubjectFactory subject = new SubjectFactory()
            .WithName("Valid Subject")
            .WithAuthor(author)
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials);

        Exception? exception = Record.Exception(() => subject.Build());

        Assert.Null(exception);
    }
}
