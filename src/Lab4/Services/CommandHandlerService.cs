using Itmo.ObjectOrientedProgramming.Lab4.Handlers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class CommandHandlerService
{
    private readonly IEnumerable<ICommandHandler> _commandHandlers;

    public CommandHandlerService(IEnumerable<ICommandHandler> commandHandlers)
    {
        _commandHandlers = commandHandlers;
    }

    public void HandleCommand(string commandLine)
    {
        string[] inputArgs = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (inputArgs.Length == 0)
        {
            throw new ArgumentException("Пожалуйста, укажите команду.");
        }

        string commandType = inputArgs[0];
        ICommandHandler? handler = _commandHandlers.FirstOrDefault(h => h.CommandName.Equals(commandType, StringComparison.OrdinalIgnoreCase));

        if (handler == null)
        {
            throw new ArgumentException($"Неизвестная команда: {commandType}");
        }

        try
        {
            Command command = handler.HandleCommand(inputArgs);
            command.Validate();
            command.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}