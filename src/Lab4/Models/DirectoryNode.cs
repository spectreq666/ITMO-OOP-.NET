namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class DirectoryNode
{
    public string Name { get; }

    public bool IsFile { get; }

    private readonly List<DirectoryNode> _children = new();

    public IReadOnlyCollection<DirectoryNode> Children => _children.AsReadOnly();

    public DirectoryNode(string name, bool isFile)
    {
        Name = name;
        IsFile = isFile;
    }

    public void AddChild(DirectoryNode child)
    {
        if (IsFile)
            throw new InvalidOperationException("Файл не может иметь дочерние узлы.");

        _children.Add(child);
    }
}