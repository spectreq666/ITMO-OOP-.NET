namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;

public interface IDisplayDriver
{
    void Clear();

    void SetColor(ConsoleColor color);

    void WriteText(string text);

    void WriteToFile(string text);
}