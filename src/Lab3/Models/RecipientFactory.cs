using Itmo.ObjectOrientedProgramming.Lab3.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public abstract class RecipientFactory
{
    public abstract IRecipient CreateRecipient();
}