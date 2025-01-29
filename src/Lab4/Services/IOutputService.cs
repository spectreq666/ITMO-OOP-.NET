using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public interface IOutputService
{
    void PrintTree(IEnumerable<DirectoryNode> tree);

    void PrintMessage(string message);
}