using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Handlers;
using Itmo.ObjectOrientedProgramming.Lab4.Services;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class ExplorerTests
{
    private readonly Mock<IFileSystemController> _mockFileSystemController;
    private readonly Mock<IOutputService> _mockOutputService;
    private readonly CommandHandlerService _commandHandlerService;

    public ExplorerTests()
    {
        _mockFileSystemController = new Mock<IFileSystemController>();
        _mockOutputService = new Mock<IOutputService>();

        var treeSubCommandHandlers = new List<ITreeSubCommandHandler>
        {
            new TreeGotoHandler(),
            new TreeListHandler(),
        };

        var fileSubCommandHandlers = new List<IFileSubCommandHandler>
        {
            new FileCopyHandler(),
            new FileDeleteHandler(),
            new FileMoveHandler(),
            new FileRenameHandler(),
            new FileShowHandler(),
        };

        var commandHandlers = new List<ICommandHandler>
        {
            new ConnectCommandHandler(_mockFileSystemController.Object),
            new DisconnectCommandHandler(_mockFileSystemController.Object),
            new TreeCommandHandler(_mockFileSystemController.Object, _mockOutputService.Object, treeSubCommandHandlers),
            new FileCommandHandler(_mockFileSystemController.Object, _mockOutputService.Object, fileSubCommandHandlers),
        };

        _commandHandlerService = new CommandHandlerService(commandHandlers);
    }

    [Fact]
    public void HandleCommand_DisconnectCommand_CorrectArguments()
    {
        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        string input = "disconnect";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.Disconnect(), Times.Once);
    }

    [Fact]
    public void HandleCommand_TreeListCommand_CorrectArguments()
    {
        string directoryPath = "tests\\Lab4.Tests\\DirectoryForTests";

        _mockFileSystemController.Setup(fs => fs.DirectoryExists(directoryPath))
            .Returns(true);

        string input = $"connect {directoryPath} -m local";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.Connect(directoryPath), Times.Once);
    }

    [Fact]
    public void HandleCommand_ChangeDirectoryCommand_CorrectArguments()
    {
        string directoryPath = "tests/Lab4.Tests/NewDirectory";

        _mockFileSystemController.Setup(fs => fs.DirectoryExists(directoryPath))
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        string input = $"tree goto {directoryPath}";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.ChangeDirectory(directoryPath), Times.Once);
    }

    [Fact]
    public void HandleCommand_ListDirectory_CorrectArguments()
    {
        string directoryPath = "tests/Lab4.Tests/NewDirectory";

        _mockFileSystemController.Setup(fs => fs.DirectoryExists(directoryPath))
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.CurrentDirectory)
            .Returns(directoryPath);

        string input = "tree list -d 1";
        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.GetDirectoryTree(directoryPath, 1), Times.Once);
    }

    [Fact]
    public void HandleCommand_FileShow_ConsoleMode_CorrectArguments()
    {
        string relativeFilePath = "file.txt";
        string fullFilePath = "tests/Lab4.Tests/ExistingDirectory/file.txt";

        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.CurrentDirectory)
            .Returns("tests/Lab4.Tests/ExistingDirectory");

        _mockFileSystemController.Setup(fs => fs.GetFullPath(relativeFilePath))
            .Returns(fullFilePath);

        _mockFileSystemController.Setup(fs => fs.FileExists(fullFilePath))
            .Returns(true);

        string input = $"file show {relativeFilePath} -m console";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.GetFileContent(fullFilePath), Times.Once);
    }

    [Fact]
    public void HandleCommand_FileDelete_CorrectArguments()
    {
        string fullFilePath = "tests/Lab4.Tests/ExistingDirectory/file.txt";

        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.CurrentDirectory)
            .Returns("tests/Lab4.Tests/ExistingDirectory");

        _mockFileSystemController.Setup(fs => fs.FileExists(fullFilePath))
            .Returns(true);

        string input = $"file delete {fullFilePath}";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.DeleteFile(fullFilePath), Times.Once);
    }

    [Fact]
    public void HandleCommand_FileCopy_CorrectArguments()
    {
        string relativeSourcePath = "file.txt";
        string fullDestinationPath = "tests/Lab4.Tests/ExistingDirectory/DestinationDirectory";

        _mockFileSystemController.Setup(fs => fs.IsConnected)
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.CurrentDirectory)
            .Returns("tests/Lab4.Tests/ExistingDirectory");

        _mockFileSystemController.Setup(fs => fs.FileExists(relativeSourcePath))
            .Returns(true);

        _mockFileSystemController.Setup(fs => fs.DirectoryExists(fullDestinationPath))
            .Returns(true);

        string input = $"file copy {relativeSourcePath} {fullDestinationPath}";

        _commandHandlerService.HandleCommand(input);

        _mockFileSystemController.Verify(fs => fs.CopyFile(relativeSourcePath, fullDestinationPath), Times.Once);
    }
}