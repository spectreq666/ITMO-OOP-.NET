using Itmo.ObjectOrientedProgramming.Lab1.Models.DTO;
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

    public ProcessTrainDto ProcessTrain(Train train)
    {
        double time = 0;
        double remainingDistance = Length;

        while (remainingDistance > 0)
        {
            if (!train.ApplyForce(Force))
            {
                return new ProcessTrainDto(time, false);
            }

            train.ApplyForce(Force);

            double resultingSpeed = train.Speed + (train.Acceleration * train.Precision);
            double traveledDistance = resultingSpeed * train.Precision;

            if (resultingSpeed < 0)
            {
                return new ProcessTrainDto(time, false);
            }

            remainingDistance -= traveledDistance;
            train.Speed = resultingSpeed;
            time += train.Precision;

            if (resultingSpeed == 0 && train.Acceleration == 0)
            {
                return new ProcessTrainDto(0, false);
            }
        }

        return new ProcessTrainDto(time, true);
    }
}