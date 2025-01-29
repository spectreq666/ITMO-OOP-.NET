namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public abstract class Command
{
    public string Name { get; }

    protected Command(string name)
    {
        Name = name;
    }

    public abstract void Validate();

    public abstract void Execute();
}