using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDeleteCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly string _filePath;

    public FileDeleteCommand(IFileSystemController fileSystem, IOutputService outputService, string address)
        : base("file delete")
    {
        _fileSystem = fileSystem;
        _outputService = outputService;

        _filePath = address;
    }

    public override void Validate()
    {
        if (!_fileSystem.IsConnected)
            throw new InvalidOperationException("Файловая система не подключена.");

        if (!_fileSystem.FileExists(_filePath))
            throw new FileNotFoundException($"Файл по пути '{_filePath}' не найден.");
    }

    public override void Execute()
    {
        _fileSystem.DeleteFile(_filePath);
        _outputService.PrintMessage($"Файл '{_filePath}' успешно удалён.");
    }
}