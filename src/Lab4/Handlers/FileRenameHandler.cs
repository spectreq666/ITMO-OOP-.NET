using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class FileRenameHandler : IFileSubCommandHandler
{
    public string CommandName => "rename";

    public Command HandleCommand(
        string[] args,
        IFileSystemController fileSystemController,
        IOutputService outputService)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Команда 'file rename' требует два аргумента: путь файла и новое имя.");
        }

        string filePath = args[2];
        string newFileName = args[3];
        return new FileRenameCommand(fileSystemController, outputService, filePath, newFileName);
    }
}