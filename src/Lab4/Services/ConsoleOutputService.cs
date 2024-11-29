using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class ConsoleOutputService : IOutputService
{
    public void PrintTree(IEnumerable<DirectoryNode> tree)
    {
        PrintTree(tree, string.Empty);
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    private void PrintTree(IEnumerable<DirectoryNode> tree, string prefix)
    {
        foreach (DirectoryNode node in tree)
        {
            Console.WriteLine($"{prefix}{(node.IsFile ? "- " : "+ ")}{node.Name}");

            if (!node.IsFile)
            {
                PrintTree(node.Children, prefix + "  ");
            }
        }
    }
}