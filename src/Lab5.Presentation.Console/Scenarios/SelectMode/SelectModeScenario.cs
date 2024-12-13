using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using Lab5.Presentation.Console.Scenarios.Admin.Login;
using Lab5.Presentation.Console.Scenarios.User.CreateAccount;
using Lab5.Presentation.Console.Scenarios.User.Login;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.SelectMode;

public class SelectModeScenario : IScenario
{
    private readonly IAccountService _accountService;
    private readonly IAdminAccountService _adminAccountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ITransactionService _transactionService;

    public SelectModeScenario(IAccountService accountService, IAdminAccountService adminAccountService, ICurrentAccountService currentAccountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _adminAccountService = adminAccountService;
        _currentAccountService = currentAccountService;
        _transactionService = transactionService;
    }

    public string Name => "Select Mode";

    public void Run()
    {
        string mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите режим работы")
                .AddChoices("Пользователь", "Администратор"));

        if (mode == "Пользователь")
        {
            RunUserMode();
        }
        else if (mode == "Администратор")
        {
            RunAdminMode();
        }
    }

    private void RunUserMode()
    {
        AnsiConsole.MarkupLine("[green]Режим пользователя активирован.[/]");

        var providers = new List<IScenarioProvider>
        {
            new LoginScenarioProvider(_accountService, _currentAccountService, _transactionService),
            new CreateAccountScenarioProvider(_accountService, _currentAccountService, _transactionService),
        };

        var userLoginScenario = new ScenarioRunner(providers);

        userLoginScenario.Run();
    }

    private void RunAdminMode()
    {
        AnsiConsole.MarkupLine("[red]Режим администратора активирован.[/]");

        var scenario = new AdminLoginScenario(_adminAccountService);
        scenario.Run();
    }
}