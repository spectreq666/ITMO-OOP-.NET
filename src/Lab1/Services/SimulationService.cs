using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public class SimulationService
{
    private readonly ITrain _train;
    private readonly Route _route;

    public SimulationService(ITrain train, Route route)
    {
        _train = train;
        _route = route;
    }

    public RouteResult StartSimulation()
    {
        RouteResult processResult = _route.Run(_train);

        if (processResult is RouteResult.Success result)
        {
            return new RouteResult.Success(result.Time);
        }

        return new RouteResult.Failure();
    }
}