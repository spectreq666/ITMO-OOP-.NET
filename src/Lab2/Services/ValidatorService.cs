using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class ValidatorService
{
    public void UpdateSubject(User author, Subject subject, string name, GradingType gradingType, IReadOnlyCollection<Lecture> lectureMaterials)
    {
        if (author != subject.Author)
        {
            throw new InvalidOperationException("Only the author can modify this subject.");
        }

        subject.Update(name, gradingType, lectureMaterials);
    }

    public void UpdateLecture(User author, Lecture lecture, string name, string description, string content)
    {
        if (author != lecture.Author)
        {
            throw new InvalidOperationException("Only the author can modify this lecture.");
        }

        lecture.Update(name, description, content);
    }

    public void UpdateLabWork(User author, LabWork labWork, string name, string description, string rateCriteria)
    {
        if (author != labWork.Author)
        {
            throw new InvalidOperationException("Only the author can modify this lab work.");
        }

        labWork.Update(name, description, rateCriteria);
    }
}