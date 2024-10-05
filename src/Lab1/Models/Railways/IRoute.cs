namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Railways;

public interface IRoute
{
    RouteResult Run(ITrain train);
}