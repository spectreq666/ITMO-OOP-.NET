using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class Group : IRecipient
{
    private readonly IReadOnlyCollection<IRecipient> _recipients;
    private ImportanceLevel _importanceFilter = ImportanceLevel.None;

    public Group(IReadOnlyCollection<IRecipient> recipients)
    {
        _recipients = new List<IRecipient>(recipients);
    }

    public void SetImportanceFilter(ImportanceLevel importanceLevel)
    {
        _importanceFilter = importanceLevel;
    }

    public void ReceiveMessage(Message message)
    {
        if (_importanceFilter != ImportanceLevel.None && message.Importance < _importanceFilter) return;
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}