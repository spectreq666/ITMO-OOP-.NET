namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public interface IRailSegment
{
    RouteResult MoveTrain(ITrain train);
}