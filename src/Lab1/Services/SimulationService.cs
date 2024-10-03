using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public class SimulationService
{
    private readonly Train _train;
    private readonly Route _route;

    public SimulationService(Train train, Route route)
    {
        _train = train;
        _route = route;
    }

    public RouteResult RunSimulation()
    {
        RouteResult processResult = _route.ProcessRoute(_train);

        if (processResult is RouteResult.Success result)
        {
            return new RouteResult.Success(result.Time);
        }

        return new RouteResult.Failure();
    }
}