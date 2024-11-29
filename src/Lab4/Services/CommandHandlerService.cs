using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class CommandHandlerService
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;

    public CommandHandlerService(IFileSystemController fileSystem, IOutputService outputService)
    {
        _fileSystem = fileSystem;
        _outputService = outputService;
    }

    public void HandleCommand(string commandLine)
    {
        string[] inputArgs = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (inputArgs.Length == 0)
        {
            throw new ArgumentException("Пожалуйста, укажите команду.");
        }

        string commandType = inputArgs[0].ToLower(System.Globalization.CultureInfo.CurrentCulture);
        string[] commandArgs = inputArgs.Skip(1).ToArray();

        Command command;

        if (commandType == "tree")
        {
            if (commandArgs[0].Equals("list", StringComparison.CurrentCultureIgnoreCase))
            {
                int depth = ParseDepthFlag(commandArgs);
                command = new TreeListCommand(_fileSystem, _outputService, depth);
            }
            else if (commandArgs[0].Equals("goto", StringComparison.CurrentCultureIgnoreCase))
            {
                if (commandArgs is not { Length: 2 })
                {
                    throw new ArgumentException("Команда 'tree goto' требует аргумент - путь.");
                }

                string path = commandArgs[1];
                command = new TreeGotoCommand(_fileSystem, path);
            }
            else
            {
                throw new ArgumentException("Неизвестная команда 'tree'.");
            }
        }
        else if (commandType == "file")
        {
            if (commandArgs[0].Equals("show", StringComparison.CurrentCultureIgnoreCase))
            {
                (string path, string mode) = ParseFileShowCommand(commandArgs);
                command = new FileShowCommand(_fileSystem, path, mode);
            }
            else if (commandArgs[0].Equals("move", StringComparison.CurrentCultureIgnoreCase))
            {
                (string sourcePath, string destinationPath) = (commandArgs[1], commandArgs[2]);
                command = new FileMoveCommand(_fileSystem, _outputService, sourcePath, destinationPath);
            }
            else if (commandArgs[0].Equals("copy", StringComparison.CurrentCultureIgnoreCase))
            {
                (string sourcePath, string destinationPath) = (commandArgs[1], commandArgs[2]);
                command = new FileCopyCommand(_fileSystem, _outputService, sourcePath, destinationPath);
            }
            else if (commandArgs[0].Equals("rename", StringComparison.CurrentCultureIgnoreCase))
            {
                string filePath = commandArgs[1];
                string newFileName = commandArgs[2];
                command = new FileRenameCommand(_fileSystem, _outputService, filePath, newFileName);
            }
            else if (commandArgs[0].Equals("delete", StringComparison.CurrentCultureIgnoreCase))
            {
                string sourcePath = commandArgs[1];
                command = new FileDeleteCommand(_fileSystem, _outputService, sourcePath);
            }
            else
            {
                throw new ArgumentException("Неизвестная команда 'file'.");
            }
        }
        else if (commandType == "connect")
        {
            if (commandArgs.Length != 3)
            {
                throw new ArgumentException("Команда 'connect' требует два аргумента: адрес и режим.");
            }

            (string Address, string Mode) parsedArgs = ParseConnectCommand(commandArgs);
            command = new ConnectCommand(_fileSystem, parsedArgs.Address, parsedArgs.Mode);
        }
        else if (commandType == "disconnect")
        {
            command = new DisconnectCommand(_fileSystem);
        }
        else
        {
            throw new ArgumentException($"Неизвестная команда: {commandType}");
        }

        try
        {
            command.Validate();
            command.Execute();
        }
        catch (Exception ex)
        {
            _outputService.PrintMessage($"Ошибка: {ex.Message}");
        }
    }

    private (string Address, string Mode) ParseConnectCommand(string[] args)
    {
        string? address = null;
        string? mode = null;

        for (int i = 0; i < args.Length; i++)
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

    private int ParseDepthFlag(string[] args)
    {
        int depth = -1;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Equals("-d", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out depth))
                {
                    return depth;
                }
            }
        }

        return depth;
    }

    private (string Path, string Mode) ParseFileShowCommand(string[] args)
    {
        if (args.Length < 3 || !args[0].Equals("show", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Команда 'file show' должна включать путь и флаг '-m'.");
        }

        int modeIndex = Array.IndexOf(args, "-m");
        if (modeIndex == -1 || modeIndex == args.Length - 1)
        {
            throw new ArgumentException("Флаг '-m' отсутствует или за ним не указан режим.");
        }

        string mode = args[modeIndex + 1];

        string path = string.Join(' ', args.Skip(1).Take(modeIndex - 1));

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Путь не может быть пустым.");
        }

        return (path, mode);
    }
}
