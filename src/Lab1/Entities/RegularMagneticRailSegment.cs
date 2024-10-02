using Itmo.ObjectOrientedProgramming.Lab1.Models.DTO;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class RegularMagneticRailSegment : IRailSegment
{
    public RegularMagneticRailSegment(double length)
    {
        Length = length;
    }

    public double Length { get; }

    public ProcessTrainDto ProcessTrain(Train train)
    {
        double time = train.CalculateTime(Length);
        if (time <= 0)
        {
            return new ProcessTrainDto(time, false);
        }

        var result = new ProcessTrainDto(train.CalculateTime(Length), true);
        return result;
    }
}