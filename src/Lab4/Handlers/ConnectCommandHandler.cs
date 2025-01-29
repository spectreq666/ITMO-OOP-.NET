using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class ConnectCommandHandler : ICommandHandler
{
    private readonly IFileSystemController _fileSystem;

    public ConnectCommandHandler(IFileSystemController fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public string CommandName => "connect";

    public Command HandleCommand(string[] args)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Команда 'connect' требует два аргумента: адрес и режим.");
        }

        (string address, string mode) = ParseArgs(args);

        return new ConnectCommand(_fileSystem, address, mode);
    }

    private (string Address, string Mode) ParseArgs(string[] args)
    {
        string? address = null;
        string? mode = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].Equals("-m", StringComparison.CurrentCultureIgnoreCase))
            {
                if (i + 1 < args.Length)
                {
                    mode = args[i + 1];
                    i++;
                }
                else
                {
                    throw new ArgumentException("Режим не указан после флага '-m'.");
                }
            }
            else
            {
                if (address != null)
                {
                    throw new ArgumentException("Адрес файловой системы не может быть указан несколько раз.");
                }

                address = args[i];
            }
        }

        if (string.IsNullOrEmpty(address))
        {
            throw new ArgumentException("Адрес файловой системы не может быть пустым.");
        }

        if (string.IsNullOrEmpty(mode))
        {
            throw new ArgumentException("Режим не может быть не указан.");
        }

        return (address, mode);
    }
}