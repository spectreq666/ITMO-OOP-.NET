using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Lab2.Tests.Fixtures;

public class TestFixture
{
    public TestFixture()
    {
        LabRepository = new GenericRepository<LabWork>();
        LectureRepository = new GenericRepository<Lecture>();
        SubjectRepository = new GenericRepository<Subject>();
        Author1 = new User("Author 1");
        Author2 = new User("Author 2");

        LabWork labWork = new LabWorkFactory()
            .WithName("Test Lab Work")
            .WithDescription("Description for Test Lab Work")
            .WithRateCriteria("Rate Criteria")
            .WithPoints(50)
            .WithAuthor(Author1)
            .Build();
        LabRepository.Add(labWork);

        Lecture lecture = new LectureFactory()
            .WithName("Test Lecture")
            .WithDescription("Description for Test Lecture")
            .WithContent("Content of the lecture")
            .WithAuthor(Author1)
            .Build();
        LectureRepository.Add(lecture);

        var labWorks = new List<LabWork>
        {
            new LabWorkFactory()
                .WithName("Lab Work 1")
                .WithDescription("Description for Lab Work 1")
                .WithRateCriteria("Criteria 1")
                .WithPoints(50)
                .WithAuthor(Author1)
                .Build(),
            new LabWorkFactory()
                .WithName("Lab Work 2")
                .WithDescription("Description for Lab Work 2")
                .WithRateCriteria("Criteria 2")
                .WithPoints(40)
                .WithAuthor(Author1)
                .Build(),
        };

        var lectures = new List<Lecture>
        {
            new LectureFactory()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content 1")
                .WithAuthor(Author1)
                .Build(),
            new LectureFactory()
                .WithName("Lecture 2")
                .WithDescription("Description for Lecture 2")
                .WithContent("Content 2")
                .WithAuthor(Author1)
                .Build(),
        };

        Subject subject = new SubjectFactory()
            .WithName("Test Subject")
            .WithAuthor(Author1)
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectures)
            .Build();
        SubjectRepository.Add(subject);
    }

    public GenericRepository<LabWork> LabRepository { get; }

    public GenericRepository<Lecture> LectureRepository { get; }

    public GenericRepository<Subject> SubjectRepository { get; }

    public User Author1 { get; }

    public User Author2 { get; }
}