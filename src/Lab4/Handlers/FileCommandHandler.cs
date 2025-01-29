using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class FileCommandHandler : ICommandHandler
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly IEnumerable<IFileSubCommandHandler> _subCommandHandlers;

    public FileCommandHandler(IFileSystemController fileSystem, IOutputService outputService, IEnumerable<IFileSubCommandHandler> subCommandHandlers)
    {
        _fileSystem = fileSystem;
        _outputService = outputService;
        _subCommandHandlers = subCommandHandlers;
    }

    public string CommandName => "file";

    public Command HandleCommand(string[] args)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Необходимо указать подкоманду (например, copy или delete).");
        }

        string subCommandName = args[1];

        IFileSubCommandHandler? subCommandHandler = _subCommandHandlers
            .FirstOrDefault(handler => handler.CommandName.Equals(subCommandName, StringComparison.OrdinalIgnoreCase));

        if (subCommandHandler == null)
        {
            throw new ArgumentException($"Неизвестная подкоманда: {subCommandName}");
        }

        return subCommandHandler.HandleCommand(args, _fileSystem, _outputService);
    }
}
