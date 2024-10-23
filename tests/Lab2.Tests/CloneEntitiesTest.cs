using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Xunit;

namespace Lab2.Tests;

public class CloneEntitiesTest
{
    [Fact]
    public void CloneLabWork_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");

        LabWork originalLabWork = new LabWorkFactory()
            .WithName("Original Lab Work")
            .WithDescription("Description for Original Lab Work")
            .WithRateCriteria("Original Criteria")
            .WithPoints(50)
            .WithAuthor(author)
            .Build();

        LabWork clonedLabWork = originalLabWork.Clone();

        Assert.Equal(originalLabWork.Id, clonedLabWork.ParentId);
    }

    [Fact]
    public void CloneLecture_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");

        Lecture originalLecture = new LectureFactory()
            .WithName("Original Lecture")
            .WithDescription("Description for Original Lecture")
            .WithContent("Original Content")
            .WithAuthor(author)
            .Build();

        Lecture clonedLecture = originalLecture.Clone();

        Assert.Equal(originalLecture.Id, clonedLecture.ParentId);
    }

    [Fact]
    public void CloneSubject_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");

        Subject originalSubject = new SubjectFactory()
            .WithName("Original Subject")
            .WithAuthor(author)
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(new List<LabWork>
            {
                new LabWorkFactory()
                    .WithName("Lab Work 1")
                    .WithDescription("Description for Lab Work 1")
                    .WithRateCriteria("Criteria 1")
                    .WithPoints(50)
                    .WithAuthor(author)
                    .Build(),
            })
            .WithLectureMaterials(new List<Lecture>
            {
                new LectureFactory()
                    .WithName("Lecture 1")
                    .WithDescription("Description for Lecture 1")
                    .WithContent("Content 1")
                    .WithAuthor(author)
                    .Build(),
            })
            .Build();

        Subject clonedSubject = originalSubject.Clone();

        Assert.Equal(originalSubject.Id, clonedSubject.ParentId);
    }
}