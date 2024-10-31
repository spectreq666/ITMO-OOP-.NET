using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Factories;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Xunit;

namespace Lab2.Tests;

public class CloneEntitiesTest
{
    [Fact]
    public void CloneLabWork_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");

        LabWork originalLabWork = new LabWorkBuilder()
            .WithName("Original Lab Work")
            .WithDescription("Description for Original Lab Work")
            .WithRateCriteria("Original Criteria")
            .WithPoints(50)
            .WithAuthor(author)
            .Build();

        LabWork clonedLabWork = originalLabWork.ShallowCopy();

        Assert.Equal(originalLabWork.Id, clonedLabWork.ParentId);
    }

    [Fact]
    public void CloneLecture_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");

        Lecture originalLecture = new LectureBuilder()
            .WithName("Original Lecture")
            .WithDescription("Description for Original Lecture")
            .WithContent("Original Content")
            .WithAuthor(author)
            .Build();

        Lecture clonedLecture = originalLecture.ShallowCopy();

        Assert.Equal(originalLecture.Id, clonedLecture.ParentId);
    }

    [Fact]
    public void CloneSubject_IdEqualToParentId_Success()
    {
        var author = new User("Author 1");
        var entityFactory = new EntityFactory(author);

        Subject originalSubject = entityFactory.CreateSubject()
            .WithName("Original Subject")
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(new List<LabWork>
            {
                entityFactory.CreateLabWork()
                    .WithName("Lab Work 1")
                    .WithDescription("Description for Lab Work 1")
                    .WithRateCriteria("Criteria 1")
                    .WithPoints(50)
                    .Build(),
            })
            .WithLectureMaterials(new List<Lecture>
            {
                entityFactory.CreateLecture()
                    .WithName("Lecture 1")
                    .WithDescription("Description for Lecture 1")
                    .WithContent("Content 1")
                    .Build(),
            })
            .Build();

        Subject clonedSubject = originalSubject.ShallowCopy();

        Assert.Equal(originalSubject.Id, clonedSubject.ParentId);
    }
}