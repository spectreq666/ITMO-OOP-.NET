using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Models.Transactions;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User;

public class UserScenario : IScenario
{
    private readonly IAccountService _userAccountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ITransactionService _transactionService;

    public UserScenario(IAccountService userAccountService, ICurrentAccountService currentAccountService, ITransactionService transactionService)
    {
        _userAccountService = userAccountService;
        _currentAccountService = currentAccountService;
        _transactionService = transactionService;
    }

    public string Name => "User";

    public void Run()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold green]Выберите действие для пользователя:[/]");
            AnsiConsole.MarkupLine("[blue]1[/] - Просмотр баланса");
            AnsiConsole.MarkupLine("[blue]2[/] - Снятие денег");
            AnsiConsole.MarkupLine("[blue]3[/] - Пополнение счета");
            AnsiConsole.MarkupLine("[blue]4[/] - Просмотр истории операций");
            AnsiConsole.MarkupLine("[blue]5[/] - Выход");

            int choice = AnsiConsole.Ask<int>("Введите [green]номер[/] выбранного действия: ");

            switch (choice)
            {
                case 1:
                    ViewBalance();
                    break;
                case 2:
                    WithdrawMoney();
                    break;
                case 3:
                    DepositMoney();
                    break;
                case 4:
                    ViewTransactionHistory();
                    break;
                case 5:
                    return;
                default:
                    AnsiConsole.MarkupLine("[bold red]Неверный выбор![/]");
                    break;
            }
        }
    }

    private void ViewBalance()
    {
        try
        {
            if (_currentAccountService.Account != null)
            {
                decimal balance = _userAccountService.GetBalance(_currentAccountService.Account.Id);
                AnsiConsole.MarkupLine($"Ваш баланс: [green]{balance}[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
    }

    private void WithdrawMoney()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Введите [green]сумму[/] для снятия: ");

        try
        {
            if (_currentAccountService.Account != null)
            {
                _transactionService.CreateTransaction(
                    _currentAccountService.Account.Id,
                    amount,
                    TransactionType.Withdrawal);
            }

            AnsiConsole.MarkupLine("[green]Операция успешно выполнена![/]");
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
    }

    private void DepositMoney()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Введите [green]сумму[/] для пополнения: ");

        try
        {
            if (_currentAccountService.Account != null)
            {
                _transactionService.CreateTransaction(
                    _currentAccountService.Account.Id,
                    amount,
                    TransactionType.Deposit);
            }

            AnsiConsole.MarkupLine("[green]Операция успешно выполнена![/]");
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
    }

    private void ViewTransactionHistory()
    {
        try
        {
            if (_currentAccountService.Account != null)
            {
                IEnumerable<Transaction>? transactions = _transactionService.GetTransactionHistory(_currentAccountService.Account.Id);
                if (transactions == null)
                {
                    AnsiConsole.MarkupLine("[yellow]История операций пуста.[/]");
                    return;
                }

                AnsiConsole.MarkupLine("[bold green]История операций:[/]");

                foreach (Transaction transaction in transactions)
                {
                    AnsiConsole.MarkupLine($"Дата: [blue]{transaction.Timestamp}[/], Тип: [blue]{transaction.Type}[/], Сумма: [green]{transaction.Amount}[/]");
                }
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
    }
}
