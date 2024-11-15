using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class DisplayFactory : RecipientFactory
{
    private readonly IDisplayDriver _driver;

    public DisplayFactory(IDisplayDriver driver)
    {
        _driver = driver;
    }

    public override IRecipient CreateRecipient()
    {
        return new Display(_driver);
    }
}