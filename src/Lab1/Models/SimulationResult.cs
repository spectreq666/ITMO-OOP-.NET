namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class SimulationResult
{
    public SimulationResult(bool isSuccess, double totalTime)
    {
        IsSuccess = isSuccess;
        TotalTime = totalTime;
    }

    public bool IsSuccess { get; }

    public double TotalTime { get; }
}