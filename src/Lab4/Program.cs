using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Handlers;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class Program
{
    public static void Main(string[] args)
    {
        IFileSystemController fileSystemController = new LocalFileSystemController();
        IOutputService outputService = new ConsoleOutputService();

        var treeSubCommandHandlers = new List<ITreeSubCommandHandler>
        {
            new TreeGotoHandler(),
            new TreeListHandler(),
        };

        var fileSubCommandHandlers = new List<IFileSubCommandHandler>
        {
            new FileCopyHandler(),
            new FileDeleteHandler(),
            new FileMoveHandler(),
            new FileRenameHandler(),
            new FileShowHandler(),
        };

        var commandHandlers = new List<ICommandHandler>
        {
            new ConnectCommandHandler(fileSystemController),
            new DisconnectCommandHandler(fileSystemController),
            new TreeCommandHandler(fileSystemController, outputService, treeSubCommandHandlers),
            new FileCommandHandler(fileSystemController, outputService, fileSubCommandHandlers),
        };

        var commandHandlerService = new CommandHandlerService(commandHandlers);

        Console.WriteLine("Введите команду:");

        while (true)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Команда не может быть пустой.");
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                commandHandlerService.HandleCommand(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}