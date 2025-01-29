using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Recipients;

public class UserRecipient : IRecipient
{
    private readonly User _user;
    private readonly List<(Message Message, bool IsRead)> _receivedMessages = new();
    private readonly ILogger _logger;

    public UserRecipient(User user, ILogger logger)
    {
        _user = user;
        _logger = logger;
    }

    public void ReceiveMessage(Message message)
    {
        _receivedMessages.Add((message, false));
        _logger.Log($"{_user.Name} получил сообщение: {message.Title}");
    }

    public void MarkMessageAsRead(Message message)
    {
        (Message Message, bool IsRead) messageStatus = _receivedMessages.FirstOrDefault(m => m.Message == message);

        if (messageStatus.Message == null)
            throw new InvalidOperationException("Сообщение не найдено.");

        if (messageStatus.IsRead)
            throw new InvalidOperationException("Сообщение уже прочитано.");

        _receivedMessages.Remove(messageStatus);
        _receivedMessages.Add((message, true));

        _logger.Log($"{_user.Name} отметил сообщение '{message.Title}' как прочитанное.");
    }

    public bool IsMessageRead(Message message)
    {
        (Message Message, bool IsRead) messageStatus = _receivedMessages.FirstOrDefault(m => m.Message == message);
        if (messageStatus.Message == null)
            throw new InvalidOperationException("Сообщение не найдено.");

        return messageStatus.IsRead;
    }
}