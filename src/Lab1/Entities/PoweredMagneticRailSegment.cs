using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class PoweredMagneticRailSegment : IRailSegment
{
    public PoweredMagneticRailSegment(double length, double force)
    {
        Length = length;
        Force = force;
    }

    public double Length { get; }

    public double Force { get; }

    public RouteResult MoveTrain(Train train)
    {
        double time = 0;
        double remainingDistance = Length;

        while (remainingDistance > 0)
        {
            bool forceApplied = train.ApplyForce(Force);

            if (!forceApplied)
            {
                return new RouteResult.Failure();
            }

            double resultingSpeed = train.Speed + (train.Acceleration * train.Precision);
            double traveledDistance = resultingSpeed * train.Precision;

            if (resultingSpeed < 0)
            {
                return new RouteResult.Failure();
            }

            remainingDistance -= traveledDistance;
            train.Speed = resultingSpeed;
            time += train.Precision;

            if (resultingSpeed == 0 && train.Acceleration == 0)
            {
                return new RouteResult.Failure();
            }
        }

        return new RouteResult.Success(time);
    }
}