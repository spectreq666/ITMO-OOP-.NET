using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class DisconnectCommandHandler : ICommandHandler
{
    private readonly IFileSystemController _fileSystem;

    public DisconnectCommandHandler(IFileSystemController fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public string CommandName => "disconnect";

    public Command HandleCommand(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Команда 'disconnect' не принимает аргументов.");
        }

        return new DisconnectCommand(_fileSystem);
    }
}