using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Handlers;

public interface ICommandHandler
{
    string CommandName { get; }

    Command HandleCommand(string[] args);
}