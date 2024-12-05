using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class FileDeleteHandler : IFileSubCommandHandler
{
    public string CommandName => "delete";

    public Command HandleCommand(string[] args, IFileSystemController fileSystemController, IOutputService outputService)
    {
        if (args.Length != 3)
        {
            throw new ArgumentException("Команда 'file delete' требует путь файла");
        }

        string address = args[2];

        return new FileDeleteCommand(fileSystemController, outputService, address);
    }
}