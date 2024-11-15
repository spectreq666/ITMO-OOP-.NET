using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class User : IRecipient
{
    private readonly List<(Message Message, bool IsRead)> _receivedMessages = [];
    private readonly ILogger _logger;
    private ImportanceLevel _importanceFilter = ImportanceLevel.None;

    public User(string name, ILogger logger)
    {
        Name = name;
        _logger = logger;
    }

    public string Name { get; }

    public void SetImportanceFilter(ImportanceLevel importanceLevel)
    {
        _importanceFilter = importanceLevel;
    }

    public void ReceiveMessage(Message message)
    {
        if (_importanceFilter != ImportanceLevel.None && message.Importance < _importanceFilter) return;
        _receivedMessages.Add((message, false));
        _logger.Log($"{Name} получил сообщение: {message.Title}");
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

        _logger.Log($"{Name} отметил сообщение '{message.Title}' как прочитанное.");
    }

    public bool IsMessageRead(Message message)
    {
        (Message Message, bool IsRead) messageStatus = _receivedMessages.FirstOrDefault(m => m.Message == message);
        if (messageStatus.Message == null)
            throw new InvalidOperationException("Сообщение не найдено.");

        return messageStatus.IsRead;
    }
}