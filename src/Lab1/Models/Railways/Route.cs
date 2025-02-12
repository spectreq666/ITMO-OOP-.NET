﻿namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public class Route : IRoute
{
    private readonly List<IRailSegment> _rails;

    private readonly double _endSpeedLimit;

    public Route(IReadOnlyCollection<IRailSegment> rails, double endSpeedLimit)
    {
        _rails = new List<IRailSegment>(rails);
        _endSpeedLimit = endSpeedLimit;
    }

    public RouteResult Run(ITrain train)
    {
        double totalTime = 0;

        foreach (IRailSegment rail in _rails)
        {
            RouteResult result = rail.MoveTrain(train);

            switch (result)
            {
                case RouteResult.Success success:
                    totalTime += success.Time;
                    break;

                case RouteResult.Failure failure:
                    return failure;
            }
        }

        if (train.Speed > _endSpeedLimit)
        {
            return new RouteResult.Failure();
        }

        train.StopMove();
        return new RouteResult.Success(totalTime);
    }
}