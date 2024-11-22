namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class Group : IRecipient
{
    private readonly IReadOnlyCollection<IRecipient> _recipients;

    public Group(IReadOnlyCollection<IRecipient> recipients)
    {
        _recipients = new List<IRecipient>(recipients);
    }

    public void ReceiveMessage(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}