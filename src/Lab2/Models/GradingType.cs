namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class GradingType
{
    public GradingType(GradingFormat type, int specifiedPoints)
    {
        Type = type;
        SpecifiedPoints = specifiedPoints;
    }

    public GradingFormat Type { get; }

    public int SpecifiedPoints { get; }
}