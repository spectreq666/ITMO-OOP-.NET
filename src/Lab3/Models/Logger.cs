namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}