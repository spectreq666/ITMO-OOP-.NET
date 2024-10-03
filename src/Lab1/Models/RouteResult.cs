namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public abstract record RouteResult
{
    private RouteResult() { }

    public sealed record Success(double Time, bool IsSuccess = true) : RouteResult;

    public sealed record Failure(double Time = 0, bool IsSuccess = false) : RouteResult;
}