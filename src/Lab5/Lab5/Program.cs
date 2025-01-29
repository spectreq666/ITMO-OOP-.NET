using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Accounts;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Transactions;
using Lab5.Infrastructure.DataAccess.Database;
using Lab5.Infrastructure.DataAccess.Repositories;
using Lab5.Presentation.Console.Scenarios.SelectMode;
using Npgsql;

namespace Lab5;

public class Program
{
    private static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=test2";
        var databaseConnection = new DatabaseConnection(connectionString);
        NpgsqlConnection connection = databaseConnection.Init();
        var currentAccountService = new CurrentAccount();

        IAccountRepository accountRepository = new AccountRepository(connection);
        ITransactionRepository transactionRepository = new TransactionRepository(connection);
        IAdminRepository adminRepository = new AdminRepository(connection);

        IAccountService accountService = new AccountService(accountRepository, currentAccountService);
        IAdminAccountService adminAccountService = new AdminAccountService(accountRepository, adminRepository);
        ITransactionService transactionService = new TransactionService(transactionRepository, accountRepository);

        var scenarioRunner = new SelectModeScenario(accountService, adminAccountService, currentAccountService, transactionService);
        scenarioRunner.Run();
    }
}