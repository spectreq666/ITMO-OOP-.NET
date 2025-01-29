using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class GroupFactory : RecipientFactory
{
    private readonly IReadOnlyCollection<IRecipient> _recipients;

    public GroupFactory(IReadOnlyCollection<IRecipient> recipients)
    {
        _recipients = recipients;
    }

    public override IRecipient CreateRecipient()
    {
        return new Group(_recipients);
    }
}