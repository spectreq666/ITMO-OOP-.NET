using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IEducationalProgramBuilder
{
    IEducationalProgramBuilder WithName(string name);

    IEducationalProgramBuilder WithLeader(User programLeader);

    IEducationalProgramBuilder WithSubject(int semester, Subject subject);

    EducationalProgram Build();
}