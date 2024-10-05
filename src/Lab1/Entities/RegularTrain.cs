using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class RegularTrain : ITrain
{
    public RegularTrain(double mass, double maxForce, double precision)
    {
        Mass = mass;
        MaxForce = maxForce;
        Precision = precision;
        Speed = 0;
        Acceleration = 0;
    }

    public double Mass { get; }

    public double MaxForce { get; }

    public double Acceleration { get; private set; }

    public double Speed { get; set; }

    public double Precision { get; }

    public bool ApplyForce(double force)
    {
        if (force > MaxForce)
        {
            return false;
        }

        Acceleration = force / Mass;
        return true;
    }

    public double CalculateTime(double distance)
    {
        double time = 0;
        double remainingDistance = distance;

        if (Speed == 0 && Acceleration == 0)
        {
            return 0;
        }

        while (remainingDistance > 0)
        {
            double resultingSpeed = Speed + (Acceleration * Precision);
            double traveledDistance = resultingSpeed * Precision;

            if (resultingSpeed < 0)
            {
                return 0;
            }

            remainingDistance -= traveledDistance;
            Speed = resultingSpeed;
            time += Precision;

            if (resultingSpeed == 0 && Acceleration == 0)
            {
                return 0;
            }
        }

        return time;
    }

    public void StopMove()
    {
        Speed = 0;
        Acceleration = 0;
    }
}