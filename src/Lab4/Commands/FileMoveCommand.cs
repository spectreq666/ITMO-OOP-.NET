using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileMoveCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(IFileSystemController fileSystem, IOutputService outputService, string sourcePath, string destinationPath)
        : base("file move")
    {
        _fileSystem = fileSystem;
        _outputService = outputService;

        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public override void Validate()
    {
        if (!_fileSystem.IsConnected)
            throw new InvalidOperationException("Файловая система не подключена.");

        if (!_fileSystem.FileExists(_sourcePath))
            throw new FileNotFoundException($"Файл по пути '{_sourcePath}' не найден.");

        if (!_fileSystem.DirectoryExists(_destinationPath))
            throw new DirectoryNotFoundException($"Директория '{_destinationPath}' не существует.");
    }

    public override void Execute()
    {
        _fileSystem.MoveFile(_sourcePath, _destinationPath);
        _outputService.PrintMessage($"Файл '{_sourcePath}' успешно перемещен в '{_destinationPath}'.");
    }
}