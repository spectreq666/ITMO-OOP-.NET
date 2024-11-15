namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public interface IDisplayDriver
{
    void Clear();

    void SetColor(ConsoleColor color);

    void WriteText(string text);

    void WriteToFile(string text);
}