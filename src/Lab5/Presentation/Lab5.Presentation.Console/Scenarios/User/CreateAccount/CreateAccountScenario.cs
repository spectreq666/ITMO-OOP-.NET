using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ITransactionService _transactionService;

    public CreateAccountScenario(IAccountService accountService, ICurrentAccountService currentAccountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
        _transactionService = transactionService;
    }

    public string Name => "Create Account";

    public void Run()
    {
        int number = AnsiConsole.Ask<int>("Enter your number");
        string password = AnsiConsole.Ask<string>("Enter your password");
        LoginResult result = _accountService.CreateAccount(number, password);

        string message = result switch
        {
            LoginResult.Success => "Successful registration",
            LoginResult.Failed => "User already exists",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);

        if (result is LoginResult.Failed)
        {
            return;
        }

        AnsiConsole.Ask<string>("Ok");

        var scenario = new UserScenario(_accountService, _currentAccountService, _transactionService);
        scenario.Run();
    }
}