using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.DTO;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public class SimulationService
{
    public SimulationService(Train train, Route route)
    {
        _train = train;
        _route = route;
    }

    private readonly Train _train;
    private readonly Route _route;

    public SimulationResult RunSimulation()
    {
        ProcessTrainDto processResult = _route.ProcessRoute(_train);

        return new SimulationResult(
            processResult.IsSuccess,
            processResult.Time);
    }
}