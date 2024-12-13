using Lab5.Application.Contracts.Accounts;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Admin.Login;

public class AdminLoginScenario : IScenario
{
    private readonly IAdminAccountService _adminAccountService;

    public AdminLoginScenario(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public string Name => "Admin Login";

    public void Run()
    {
        string adminPassword = AnsiConsole.Ask<string>("Enter the [green]admin password[/]: ");

        LoginResult result = _adminAccountService.Login(adminPassword);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.Failed => "Login failed",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        if (result is LoginResult.Failed)
        {
            return;
        }

        var scenario = new AdminScenario(_adminAccountService);
        scenario.Run();
    }
}