using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User.Login;

public class LoginScenario : IScenario
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ITransactionService _transactionService;

    public LoginScenario(IAccountService accountService, ICurrentAccountService currentAccountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
        _transactionService = transactionService;
    }

    public string Name => "Login";

    public void Run()
    {
        int accountNumber = AnsiConsole.Ask<int>("Enter the [green]account number[/]: ");
        string password = AnsiConsole.Ask<string>("Enter the [green]password[/]: ");

        LoginResult result = _accountService.Login(accountNumber, password);

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

        var scenario = new UserScenario(_accountService, _currentAccountService, _transactionService);
        scenario.Run();
    }
}