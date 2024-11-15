namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class DisplayDriver : IDisplayDriver
{
    private ConsoleColor _currentColor;

    public DisplayDriver()
    {
        _currentColor = ConsoleColor.White;
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void SetColor(ConsoleColor color)
    {
        _currentColor = color;
        Console.ForegroundColor = _currentColor;
    }

    public void WriteText(string text)
    {
        Console.WriteLine(text);
        WriteToFile(text);
    }

    public void WriteToFile(string text)
    {
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Lab3.Tests", "Output");
        try
        {
            File.AppendAllText($"{outputFolder}/log.txt", $"{text}{Environment.NewLine}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}