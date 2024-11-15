using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Displays;

public class Display : IRecipient
{
    private readonly IDisplayDriver _driver;
    private ImportanceLevel _importanceFilter = ImportanceLevel.None;

    public Display(IDisplayDriver driver)
    {
        _driver = driver;
    }

    public void SetImportanceFilter(ImportanceLevel importanceLevel)
    {
        _importanceFilter = importanceLevel;
    }

    public void ReceiveMessage(Message message)
    {
        if (_importanceFilter != ImportanceLevel.None && message.Importance < _importanceFilter) return;
        _driver.Clear();
        _driver.SetColor(ConsoleColor.Red);
        _driver.WriteText($"Title: {message.Title}\nBody: {message.Body}");
    }
}