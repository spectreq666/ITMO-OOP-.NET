using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class FileShowHandler : IFileSubCommandHandler
{
    public string CommandName => "show";

    public Command HandleCommand(
        string[] args,
        IFileSystemController fileSystemController,
        IOutputService outputService)
    {
        if (args.Length != 5)
        {
            throw new ArgumentException("Команда 'file show' требует аргументы: путь файла, -m режим показа.");
        }

        if (!args[3].Equals("-m", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Отсутствует флаг режима -m");
        }

        string filePath = args[2];
        string mode = args[4];
        return new FileShowCommand(fileSystemController, outputService, filePath, mode);
    }
}