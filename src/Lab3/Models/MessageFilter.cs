using Itmo.ObjectOrientedProgramming.Lab3.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class MessageFilter : IRecipient
{
    private readonly IRecipient _recipient;
    private ImportanceLevel _importanceFilter = ImportanceLevel.None;

    public MessageFilter(IRecipient recipient)
    {
        _recipient = recipient;
    }

    public void SetImportanceFilter(ImportanceLevel importanceLevel)
    {
        _importanceFilter = importanceLevel;
    }

    public void ReceiveMessage(Message message)
    {
        if (_importanceFilter != ImportanceLevel.None && message.Importance < _importanceFilter)
            return;

        _recipient.ReceiveMessage(message);
    }
}