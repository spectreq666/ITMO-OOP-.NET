namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IPrototype<out T> where T : IEntity
{
    T Clone();
}