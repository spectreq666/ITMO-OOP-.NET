using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeListCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly IOutputService _outputService;
    private readonly int _depth;

    public TreeListCommand(IFileSystemController fileSystem, IOutputService outputService, int depth)
        : base("tree list")
    {
        _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        _outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));

        _depth = depth;
    }

    public override void Validate()
    {
        if (_depth < 1)
        {
            throw new ArgumentException("Depth must be greater than 1.");
        }

        if (!_fileSystem.IsConnected)
            throw new InvalidOperationException("Файловая система не подключена.");
    }

    public override void Execute()
    {
        IEnumerable<DirectoryNode> treeStructure = _fileSystem.GetDirectoryTree(_fileSystem.CurrentDirectory, _depth);
        _outputService.PrintTree(treeStructure);
    }
}
