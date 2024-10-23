namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IBuilder<out T>
{
    T Build();
}