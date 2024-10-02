using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Models.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public class Route
{
    public Route(IReadOnlyCollection<IRailSegment> rails, double endSpeedLimit)
    {
        _rails = new List<IRailSegment>(rails);
        _endSpeedLimit = endSpeedLimit;
    }

    private readonly List<IRailSegment> _rails;

    private readonly double _endSpeedLimit;

    public ProcessTrainDto ProcessRoute(Train train)
    {
        double totalTime = 0;

        foreach (IRailSegment rail in _rails)
        {
            ProcessTrainDto result = rail.ProcessTrain(train);

            if (!result.IsSuccess)
            {
                return new ProcessTrainDto(0, false);
            }

            totalTime += result.Time;
        }

        if (train.Speed > _endSpeedLimit)
        {
            return new ProcessTrainDto(0, false);
        }

        train.Stop();
        return new ProcessTrainDto(totalTime, true);
    }
}