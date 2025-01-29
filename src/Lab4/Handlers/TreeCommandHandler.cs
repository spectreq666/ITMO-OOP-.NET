using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class TreeCommandHandler : ICommandHandler
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly IEnumerable<ITreeSubCommandHandler> _subCommandHandlers;

    public TreeCommandHandler(IFileSystemController fileSystem, IOutputService outputService, IEnumerable<ITreeSubCommandHandler> subCommandHandlers)
    {
        _fileSystem = fileSystem;
        _outputService = outputService;
        _subCommandHandlers = subCommandHandlers;
    }

    public string CommandName => "tree";

    public Command HandleCommand(string[] args)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Необходимо указать подкоманду (например, list или goto).");
        }

        string subCommandName = args[1];

        ITreeSubCommandHandler? subCommandHandler = _subCommandHandlers
            .FirstOrDefault(handler => handler.CommandName.Equals(subCommandName, StringComparison.OrdinalIgnoreCase));

        if (subCommandHandler == null)
        {
            throw new ArgumentException($"Неизвестная подкоманда: {subCommandName}");
        }

        return subCommandHandler.HandleCommand(args, _fileSystem, _outputService);
    }
}