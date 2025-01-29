using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Services;
using Lab2.Tests.Fixtures;
using Xunit;

namespace Lab2.Tests;

public class UpdateEntitiesTest : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly ValidatorService _validator;

    public UpdateEntitiesTest(TestFixture fixture)
    {
        _fixture = fixture;
        _validator = new ValidatorService();
    }

    [Fact]
    public void UpdateLabWork_WhenAuthorIsOriginalAuthor_Success()
    {
        LabWork labWork = _fixture.LabRepository.GetFromId(1);
        User author = _fixture.Author1;

        _validator.UpdateLabWork(author, labWork, "Updated Lab Work", "Updated Description", "Updated Rate Criteria");

        Assert.Equal("Updated Lab Work", labWork.Name);
        Assert.Equal("Updated Description", labWork.Description);
        Assert.Equal("Updated Rate Criteria", labWork.RateCriteria);
    }

    [Fact]
    public void UpdateLabWork_WhenAuthorIsNotOriginalAuthor_Failed()
    {
        LabWork labWork = _fixture.LabRepository.GetFromId(1);
        User nonAuthor = _fixture.Author2;

        Assert.Throws<InvalidOperationException>(() =>
            _validator.UpdateLabWork(nonAuthor, labWork, "New Lab Work", "New Description", "New Rate Criteria"));
    }

    [Fact]
    public void UpdateLecture_WhenAuthorIsOriginalAuthor_Success()
    {
        Lecture lecture = _fixture.LectureRepository.GetFromId(1);
        User author = _fixture.Author1;

        _validator.UpdateLecture(author, lecture, "Updated Lecture", "Updated Description", "Updated Content");

        Assert.Equal("Updated Lecture", lecture.Name);
        Assert.Equal("Updated Description", lecture.Description);
        Assert.Equal("Updated Content", lecture.Content);
    }

    [Fact]
    public void UpdateLecture_WhenAuthorIsNotOriginalAuthor_Failed()
    {
        Lecture lecture = _fixture.LectureRepository.GetFromId(1);
        User nonAuthor = _fixture.Author2;

        Assert.Throws<InvalidOperationException>(() =>
            _validator.UpdateLecture(nonAuthor, lecture, "New Lecture", "New Description", "New Content"));
    }

    [Fact]
    public void UpdateSubject_WhenAuthorIsOriginalAuthor_Success()
    {
        Subject subject = _fixture.SubjectRepository.GetFromId(1);
        User author = _fixture.Author1;

        _validator.UpdateSubject(author, subject, "Updated Subject", new GradingType(GradingFormat.Exam, 60), new List<Lecture>());

        Assert.Equal("Updated Subject", subject.Name);
    }

    [Fact]
    public void UpdateSubject_WhenAuthorIsNotOriginalAuthor_Failed()
    {
        Subject subject = _fixture.SubjectRepository.GetFromId(1);
        User nonAuthor = _fixture.Author2;

        Assert.Throws<InvalidOperationException>(() =>
            _validator.UpdateSubject(nonAuthor, subject, "New Subject", new GradingType(GradingFormat.Exam, 60), new List<Lecture>()));
    }
}
