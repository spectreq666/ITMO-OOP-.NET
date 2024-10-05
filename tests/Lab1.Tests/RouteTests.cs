using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;
using Itmo.ObjectOrientedProgramming.Lab1.Services;
using Xunit;
using Xunit.Abstractions;

namespace Lab1.Tests;

public class RouteTests
{
    private readonly ITestOutputHelper _output;

    public RouteTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessRoute_TrainSpeed_ToRouteMaxSpeed_Success()
    {
        var train = new RegularTrain(2000, 2000, 0.01);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(400, 1000), new RegularMagneticRailSegment(100) }, 100);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Success>(simulationResult);
        var result = (RouteResult.Success)simulationResult;

        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_TrainSpeed_OverMaxRouteSpeed_Fails()
    {
        var train = new RegularTrain(1000, 500, 0.1f);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(400, 300), new RegularMagneticRailSegment(100) }, 10);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Failure>(simulationResult);
        var result = (RouteResult.Failure)simulationResult;

        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_TrainToAllowedSpeed_AndPassesStation_Success()
    {
        var train = new RegularTrain(1500, 800, 0.5);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(1000, 610), new RegularMagneticRailSegment(1000), new Station(5,  15, 50, 50), new RegularMagneticRailSegment(150) }, 50);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Success>(simulationResult);
        var result = (RouteResult.Success)simulationResult;

        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_TrainSpeed_OverStationSpeed_Fails()
    {
        var train = new RegularTrain(100, 800, 0.01f);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(1000, 600), new Station(100, 15, 80, 50), new RegularMagneticRailSegment(300) }, 50);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Failure>(simulationResult);
        var result = (RouteResult.Failure)simulationResult;

        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_TrainSpeed_OverMaxRouteSpeed_WithStation_Fails()
    {
        var train = new RegularTrain(1000, 1000, 0.1);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(1000, 1000), new RegularMagneticRailSegment(100), new Station(150, 20, 100, 80), new RegularMagneticRailSegment(1000) }, 40);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Failure>(simulationResult);
        var result = (RouteResult.Failure)simulationResult;

        Assert.False(result.IsSuccess);
        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_TrainSpeed_SlowToStationAndRouteSpeed_Success()
    {
        var train = new RegularTrain(1000, 1500, 1);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(1000, 1500), new RegularMagneticRailSegment(100), new PoweredMagneticRailSegment(1000, -200), new Station(200, 30, 60, 90), new RegularMagneticRailSegment(400), new PoweredMagneticRailSegment(1000, 1500), new RegularMagneticRailSegment(300), new PoweredMagneticRailSegment(1000, -500) }, 90);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Success>(simulationResult);
        var result = (RouteResult.Success)simulationResult;

        Assert.True(result.IsSuccess);
        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_ZeroStartSpeed_Fails()
    {
        var train = new RegularTrain(1000, 1500, 1);
        var route = new Route(new List<IRailSegment> { new RegularMagneticRailSegment(100) }, 100);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Failure>(simulationResult);
        var result = (RouteResult.Failure)simulationResult;

        Assert.False(result.IsSuccess);
        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }

    [Fact]
    public void ProcessRoute_NegativeForce_AfterPositive_Fails()
    {
        var train = new RegularTrain(1500, 600, 0.1);
        var route = new Route(new List<IRailSegment> { new PoweredMagneticRailSegment(400, 200), new PoweredMagneticRailSegment(400, -400) }, 100);
        var simulationService = new SimulationService(train, route);

        RouteResult simulationResult = simulationService.StartSimulation();

        Assert.IsType<RouteResult.Failure>(simulationResult);
        var result = (RouteResult.Failure)simulationResult;

        Assert.False(result.IsSuccess);
        _output.WriteLine($"Is Success?: {result.IsSuccess}");
        _output.WriteLine($"Total Time: {result.Time}");
    }
}
