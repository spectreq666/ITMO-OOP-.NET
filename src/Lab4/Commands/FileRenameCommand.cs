using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileRenameCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly string _filePath;
    private readonly string _newFileName;

    public FileRenameCommand(IFileSystemController fileSystem, IOutputService outputService, string address, string newName)
        : base("file rename")
    {
        _fileSystem = fileSystem;
        _outputService = outputService;

        _filePath = address;
        _newFileName = newName;
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
        _fileSystem.RenameFile(_filePath, _newFileName);

        _outputService.PrintMessage($"Файл '{_filePath}' переименован в '{_newFileName}'.");
    }
}