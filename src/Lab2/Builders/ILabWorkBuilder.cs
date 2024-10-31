using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface ILabWorkBuilder
{
    ILabWorkBuilder WithName(string name);

    ILabWorkBuilder WithDescription(string description);

    ILabWorkBuilder WithRateCriteria(string rateCriteria);

    ILabWorkBuilder WithPoints(int points);

    ILabWorkBuilder WithAuthor(User user);

    LabWork Build();
}