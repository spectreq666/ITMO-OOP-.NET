namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}