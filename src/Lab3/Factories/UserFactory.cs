using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class UserFactory : RecipientFactory
{
    private readonly string _name;
    private readonly ILogger _logger;

    public UserFactory(string name, ILogger logger)
    {
        _name = name;
        _logger = logger;
    }

    public override IRecipient CreateRecipient()
    {
        return new User(_name, _logger);
    }
}