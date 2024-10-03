using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public interface IRailSegment
{
    RouteResult MoveTrain(Train train);
}