using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Messengers;

public class Messenger : IRecipient
{
    private readonly ILogger _logger;
    private ImportanceLevel _importanceFilter = ImportanceLevel.None;

    public Messenger(ILogger logger)
    {
        _logger = logger;
    }

    public void SetImportanceFilter(ImportanceLevel importanceLevel)
    {
        _importanceFilter = importanceLevel;
    }

    public void ReceiveMessage(Message message)
    {
        if (_importanceFilter != ImportanceLevel.None && message.Importance < _importanceFilter) return;
        _logger.Log($"[Мессенджер] {message.Title}: {message.Body}");
    }
}