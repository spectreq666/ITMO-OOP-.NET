using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Factories;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Lab2.Tests.Fixtures;

public class TestFixture
{
    public TestFixture()
    {
        Author1 = new User("Author 1");
        Author2 = new User("Author 2");

        var entityFactory = new EntityFactory(Author1);

        LabRepository = new Repository<LabWork>();
        LectureRepository = new Repository<Lecture>();
        SubjectRepository = new Repository<Subject>();

        LabWork labWork = entityFactory.CreateLabWork()
            .WithName("Test Lab Work")
            .WithDescription("Description for Test Lab Work")
            .WithRateCriteria("Rate Criteria")
            .WithPoints(50)
            .Build();
        LabRepository.Add(labWork);

        Lecture lecture = entityFactory.CreateLecture()
            .WithName("Test Lecture")
            .WithDescription("Description for Test Lecture")
            .WithContent("Content of the lecture")
            .Build();
        LectureRepository.Add(lecture);

        var labWorks = new List<LabWork>
        {
            entityFactory.CreateLabWork()
                .WithName("Lab Work 1")
                .WithDescription("Description for Lab Work 1")
                .WithRateCriteria("Criteria 1")
                .WithPoints(50)
                .Build(),
            entityFactory.CreateLabWork()
                .WithName("Lab Work 2")
                .WithDescription("Description for Lab Work 2")
                .WithRateCriteria("Criteria 2")
                .WithPoints(40)
                .Build(),
        };

        var lectures = new List<Lecture>
        {
            entityFactory.CreateLecture()
                .WithName("Lecture 1")
                .WithDescription("Description for Lecture 1")
                .WithContent("Content 1")
                .Build(),
            entityFactory.CreateLecture()
                .WithName("Lecture 2")
                .WithDescription("Description for Lecture 2")
                .WithContent("Content 2")
                .Build(),
        };

        Subject subject = entityFactory.CreateSubject()
            .WithName("Test Subject")
            .WithGradingType(GradingFormat.Exam, 60)
            .WithTotalPoints(100)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectures)
            .Build();
        SubjectRepository.Add(subject);
    }

    public Repository<LabWork> LabRepository { get; }

    public Repository<Lecture> LectureRepository { get; }

    public Repository<Subject> SubjectRepository { get; }

    public User Author1 { get; }

    public User Author2 { get; }
}