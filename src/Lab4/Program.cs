using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class Program
{
    private static void Main(string[] args)
    {
        IFileSystemController fileSystemController = new LocalFileSystemController();
        IOutputService outputService = new ConsoleOutputService();

        var commandHandler = new CommandHandlerService(fileSystemController, outputService);

        Console.WriteLine("Введите команду:");

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Команда не может быть пустой.");
        }

        try
        {
            if (input != null) commandHandler.HandleCommand(input);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}