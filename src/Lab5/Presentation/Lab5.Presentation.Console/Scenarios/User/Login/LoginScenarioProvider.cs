using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentUser;
    private readonly ITransactionService _transactionService;

    public LoginScenarioProvider(IAccountService service, ICurrentAccountService currentUser, ITransactionService transactionService)
    {
        _service = service;
        _currentUser = currentUser;
        _transactionService = transactionService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginScenario(_service, _currentUser, _transactionService);
        return true;
    }
}