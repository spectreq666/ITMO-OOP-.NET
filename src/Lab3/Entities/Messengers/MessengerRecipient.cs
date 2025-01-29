using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Messengers;

public class MessengerRecipient : IRecipient
{
    private readonly Messenger _messenger;
    private readonly ILogger _logger;

    public MessengerRecipient(Messenger messenger, ILogger logger)
    {
        _messenger = messenger;
        _logger = logger;
    }

    public void ReceiveMessage(Message message)
    {
        _logger.Log($"[Мессенджер {_messenger.Name}] {message.Title}: {message.Body}");
    }
}