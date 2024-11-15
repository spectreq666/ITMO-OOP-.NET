using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class MessengerFactory : RecipientFactory
{
    private readonly ILogger _logger;

    public MessengerFactory(ILogger logger)
    {
        _logger = logger;
    }

    public override IRecipient CreateRecipient()
    {
        return new Messenger(_logger);
    }
}