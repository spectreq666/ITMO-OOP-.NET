namespace Itmo.ObjectOrientedProgramming.Lab3.Entities;

public class Topic
{
    private readonly List<IRecipient> _recipients = new List<IRecipient>();

    public Topic(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void SendMessage(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}