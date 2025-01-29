namespace Lab5.Application.Contracts.Accounts;

public interface IAccountService
{
    LoginResult CreateAccount(int number, string password);

    LoginResult Login(int number, string password);

    decimal GetBalance(Guid accountId);
}