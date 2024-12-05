using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public class TreeListHandler : ITreeSubCommandHandler
{
    public string CommandName => "list";

    public Command HandleCommand(
        string[] args,
        IFileSystemController fileSystemController,
        IOutputService outputService)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Команда 'tree list' принимает аргументы: -d глубина в целом числе");
        }

        if (!args[2].Equals("-d", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Не указан флаг -d");
        }

        if (!int.TryParse(args[3], out int depth))
        {
            throw new ArgumentException("Аргумент после флага -d должен быть целым числом.");
        }

        return new TreeListCommand(fileSystemController, outputService, depth);
    }
}