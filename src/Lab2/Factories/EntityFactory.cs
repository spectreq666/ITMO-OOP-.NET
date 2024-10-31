using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Factories;

public class EntityFactory
{
    private readonly User _user;

    public EntityFactory(User currentUser)
    {
        _user = currentUser;
    }

    public ILectureBuilder CreateLecture()
    {
        var lectureBuilder = new LectureBuilder();
        lectureBuilder.WithAuthor(_user);
        return lectureBuilder;
    }

    public ISubjectBuilder CreateSubject()
    {
        var subjectBuilder = new SubjectBuilder();
        subjectBuilder.WithAuthor(_user);
        return subjectBuilder;
    }

    public ILabWorkBuilder CreateLabWork()
    {
        var labWorkBuilder = new LabWorkBuilder();
        labWorkBuilder.WithAuthor(_user);
        return labWorkBuilder;
    }

    public IEducationalProgramBuilder CreateEducationalProgram()
    {
        var educationalProgramBuilder = new EducationalProgramBuilder();
        educationalProgramBuilder.WithLeader(_user);
        return educationalProgramBuilder;
    }
}