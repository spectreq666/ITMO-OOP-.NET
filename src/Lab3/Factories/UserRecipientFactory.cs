using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class UserRecipientFactory : RecipientFactory
{
    private readonly User _user;
    private readonly ILogger _logger;

    public UserRecipientFactory(User user, ILogger logger)
    {
        _user = user;
        _logger = logger;
    }

    public override IRecipient CreateRecipient()
    {
        return new UserRecipient(_user, _logger);
    }
}