using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class FileCopyHandler : IFileSubCommandHandler
{
    public string CommandName => "copy";

    public Command HandleCommand(string[] args, IFileSystemController fileSystemController, IOutputService outputService)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Команда 'file copy' требует два аргумента: исходный путь и путь назначения.");
        }

        string sourcePath = args[2];
        string destinationPath = args[3];

        return new FileCopyCommand(fileSystemController, outputService, sourcePath, destinationPath);
    }
}