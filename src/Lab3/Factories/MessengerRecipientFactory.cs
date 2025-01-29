using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Messengers;

using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class MessengerRecipientFactory : RecipientFactory
{
    private readonly ILogger _logger;
    private readonly Messenger _messenger;

    public MessengerRecipientFactory(Messenger messenger, ILogger logger)
    {
        _messenger = messenger;
        _logger = logger;
    }

    public override IRecipient CreateRecipient()
    {
        return new MessengerRecipient(_messenger, _logger);
    }
}