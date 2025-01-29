namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;

public class DisplayRecipient : IRecipient
{
    private readonly Display _display;

    public DisplayRecipient(Display display)
    {
        _display = display;
    }

    public void ReceiveMessage(Message message)
    {
        _display.Driver.Clear();
        _display.Driver.SetColor(ConsoleColor.Red);
        _display.Driver.WriteText($"{_display.Name}\nTitle: {message.Title}\nBody: {message.Body}");
    }
}