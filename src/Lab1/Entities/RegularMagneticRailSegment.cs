using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class RegularMagneticRailSegment : IRailSegment
{
    public RegularMagneticRailSegment(double length)
    {
        Length = length;
    }

    public double Length { get; }

    public RouteResult MoveTrain(Train train)
    {
        double time = train.CalculateTime(Length);
        if (time <= 0)
        {
            return new RouteResult.Failure();
        }

        return new RouteResult.Success(time);
    }
}