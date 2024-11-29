using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileShowCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly string _filePath;
    private readonly string _mode;

    public FileShowCommand(IFileSystemController fileSystem, string address, string mode)
        : base("file show")
    {
        _fileSystem = fileSystem;
        _filePath = address;
        _mode = mode;
    }

    public override void Validate()
    {
        if (_mode != "console")
        {
            throw new InvalidOperationException("Поддерживается только режим 'console'.");
        }

        string fullPath = _fileSystem.GetFullPath(_filePath);

        if (!_fileSystem.FileExists(fullPath))
        {
            throw new FileNotFoundException($"Файл {fullPath} не найден.");
        }
    }

    public override void Execute()
    {
        string fullPath = _fileSystem.GetFullPath(_filePath);
        _fileSystem.ShowFileContent(fullPath);
    }
}