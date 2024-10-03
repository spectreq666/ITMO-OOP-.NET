using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Station : IRailSegment
{
    public Station(double unloadTime, double loadTime, double maxSpeedLimit, int passengers)
    {
        UnloadTime = unloadTime;
        LoadTime = loadTime;
        MaxSpeedLimit = maxSpeedLimit;
        Passangers = passengers;
    }

    public double UnloadTime { get; }

    public double LoadTime { get; }

    public double Passangers { get; }

    public double MaxSpeedLimit { get; }

    public RouteResult MoveTrain(Train train)
    {
        double currentSpeed = train.Speed;
        if (currentSpeed > MaxSpeedLimit)
        {
            return new RouteResult.Failure();
        }

        train.Stop();
        train.Speed = currentSpeed;

        double stationTime = (UnloadTime * (Passangers / 2)) + (LoadTime * (Passangers - (Passangers / 2)));

        return new RouteResult.Success(stationTime);
    }
}