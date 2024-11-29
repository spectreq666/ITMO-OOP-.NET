using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileCopyCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly string _sourcePath;
    private readonly string _targetPath;

    public FileCopyCommand(IFileSystemController fileSystem, IOutputService outputService, string sourcePath, string destinationPath)
        : base("file copy")
    {
        _fileSystem = fileSystem;
        _outputService = outputService;
        _sourcePath = sourcePath;
        _targetPath = destinationPath;
    }

    public override void Validate()
    {
        if (!_fileSystem.IsConnected)
            throw new InvalidOperationException("Файловая система не подключена.");

        if (!_fileSystem.FileExists(_sourcePath))
            throw new FileNotFoundException($"Файл по пути '{_sourcePath}' не найден.");

        if (!_fileSystem.DirectoryExists(_targetPath))
            throw new DirectoryNotFoundException($"Директория '{_targetPath}' не существует.");
    }

    public override void Execute()
    {
        _fileSystem.CopyFile(_sourcePath, _targetPath);
        _outputService.PrintMessage($"Файл '{_sourcePath}' успешно скопирован в '{_targetPath}'.");
    }
}