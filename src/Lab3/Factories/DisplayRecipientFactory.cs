using Itmo.ObjectOrientedProgramming.Lab3.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class DisplayRecipientFactory : RecipientFactory
{
    private readonly Display _display;

    public DisplayRecipientFactory(Display display)
    {
        _display = display;
    }

    public override IRecipient CreateRecipient()
    {
        return new DisplayRecipient(_display);
    }
}