using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User.CreateAccount;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentUser;
    private readonly ITransactionService _transactionService;

    public CreateAccountScenarioProvider(IAccountService service, ICurrentAccountService currentUser, ITransactionService transactionService)
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

        scenario = new CreateAccountScenario(_service, _currentUser, _transactionService);
        return true;
    }
}