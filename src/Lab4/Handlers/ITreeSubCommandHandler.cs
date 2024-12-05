using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public interface ITreeSubCommandHandler
{
    string CommandName { get; }

    Command HandleCommand(string[] args, IFileSystemController fileSystemController, IOutputService outputService);
}