using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ConnectCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly string _address;
    private readonly string _mode;

    public ConnectCommand(IFileSystemController fileSystem, string address, string mode)
        : base("connect")
    {
        _fileSystem = fileSystem;
        _address = address;
        _mode = mode;
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_address))
            throw new ArgumentException("Параметр Address не может быть пустым.");

        if (_mode != "local")
            throw new NotSupportedException("Поддерживается только режим 'local'.");
    }

    public override void Execute()
    {
        _fileSystem.Connect(_address);
    }
}