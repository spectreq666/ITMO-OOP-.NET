using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class DisconnectCommand : Command
{
    private readonly IFileSystemController _fileSystem;

    public DisconnectCommand(IFileSystemController fileSystem)
        : base("disconnect")
    {
        _fileSystem = fileSystem;
    }

    public override void Validate()
    {
        if (!_fileSystem.IsConnected)
        {
            throw new InvalidOperationException("Файловая система не подключена.");
        }
    }

    public override void Execute()
    {
        _fileSystem.Disconnect();
    }
}