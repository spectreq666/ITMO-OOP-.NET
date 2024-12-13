using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Admin;

public class AdminScenario : IScenario
{
    private readonly IAdminAccountService _adminAccountService;

    public AdminScenario(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public string Name => "Admin";

    public void Run()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold green]Выберите действие для администратора:[/]");
            AnsiConsole.MarkupLine("[blue]1[/] - Просмотр баланса пользователя по номеру");
            AnsiConsole.MarkupLine("[blue]2[/] - Получить аккаунт пользователя по номеру");
            AnsiConsole.MarkupLine("[blue]3[/] - Изменить админ пароль");
            AnsiConsole.MarkupLine("[blue]4[/] - Выход");

            int choice = AnsiConsole.Ask<int>("Введите [green]номер[/] выбранного действия: ");

            switch (choice)
            {
                case 1:
                    ViewBalance();
                    break;
                case 2:
                    GetAccount();
                    break;
                case 3:
                    ChangeMasterKey();
                    break;
                case 4:
                    return;
                default:
                    AnsiConsole.MarkupLine("[bold red]Неверный выбор![/]");
                    break;
            }
        }
    }

    private void ViewBalance()
    {
        int username = AnsiConsole.Ask<int>("Введите [green]номер[/] пользователя для просмотра баланса: ");

        try
        {
            decimal amount = _adminAccountService.GetBalanceByNumber(username);
            AnsiConsole.MarkupLine($"Баланс пользователя: {amount}");
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message}");
        }
    }

    private void GetAccount()
    {
        int number = AnsiConsole.Ask<int>("Введите [green]номер[/] пользователя для просмотра баланса: ");

        try
        {
            Account? account = _adminAccountService.GetAccountByNumber(number);
            if (account != null)
            {
                AnsiConsole.MarkupLine($"[green]Найден аккаунт:[/]");
                AnsiConsole.MarkupLine($"ID: [blue]{account.Id}[/]");
                AnsiConsole.MarkupLine($"Номер: [blue]{account.Number}[/]");
                AnsiConsole.MarkupLine($"Баланс: [green]{account.Balance}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Аккаунт не найден.[/]");
            }
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Ошибка:[/] {ex.Message.EscapeMarkup()}");
        }
    }

    private void ChangeMasterKey()
    {
        string masterKey = AnsiConsole.Ask<string>("Введите [green]новый MasterKey[/] для администраторов: ");

        _adminAccountService.UpdateMasterKey(masterKey);
        AnsiConsole.MarkupLine($"Успешно изменили админ-пароль на [blue]{masterKey}[/]");
    }
}