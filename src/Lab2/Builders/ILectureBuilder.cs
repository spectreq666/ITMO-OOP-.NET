using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface ILectureBuilder
{
    ILectureBuilder WithName(string name);

    ILectureBuilder WithDescription(string description);

    ILectureBuilder WithContent(string content);

    ILectureBuilder WithAuthor(User author);

    Lecture Build();
}