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
        Passengers = passengers;
    }

    public double UnloadTime { get; }

    public double LoadTime { get; }

    public double Passengers { get; }

    public double MaxSpeedLimit { get; }

    public RouteResult MoveTrain(ITrain train)
    {
        double currentSpeed = train.Speed;
        if (currentSpeed > MaxSpeedLimit)
        {
            return new RouteResult.Failure();
        }

        train.StopMove();
        train.Speed = currentSpeed;

        double stationTime = (UnloadTime * (Passengers / 2)) + (LoadTime * (Passengers - (Passengers / 2)));

        return new RouteResult.Success(stationTime);
    }
}