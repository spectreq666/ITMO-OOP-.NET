namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;

public class Display
{
    public Display(string name, IDisplayDriver driver)
    {
        Name = name;
        Driver = driver;
    }

    public string Name { get; }

    public IDisplayDriver Driver { get; }
}