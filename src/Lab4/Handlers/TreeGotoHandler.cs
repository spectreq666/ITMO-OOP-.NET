using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class TreeGotoHandler : ITreeSubCommandHandler
{
    public string CommandName => "goto";

    public Command HandleCommand(
        string[] args,
        IFileSystemController fileSystemController,
        IOutputService outputService)
    {
        if (args.Length != 3)
        {
            throw new ArgumentException("Команда 'tree goto' принимает аргументы: путь");
        }

        string address = args[2];
        return new TreeGotoCommand(fileSystemController, outputService, address);
    }
}