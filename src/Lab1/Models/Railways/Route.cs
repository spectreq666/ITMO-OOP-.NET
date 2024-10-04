using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public class Route
{
    private readonly List<IRailSegment> _rails;

    private readonly double _endSpeedLimit;

    public Route(IReadOnlyCollection<IRailSegment> rails, double endSpeedLimit)
    {
        _rails = new List<IRailSegment>(rails);
        _endSpeedLimit = endSpeedLimit;
    }

    public RouteResult ProcessRoute(Train train)
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

        train.Stop();
        return new RouteResult.Success(totalTime);
    }
}