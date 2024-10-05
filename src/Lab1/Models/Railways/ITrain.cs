namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public interface ITrain
{
    public double Mass { get; }

    public double MaxForce { get; }

    public double Precision { get; }

    public double Speed { get; set; }

    public double Acceleration { get; }

    public bool ApplyForce(double force);

    public double CalculateTime(double distance);

    public void StopMove();
}