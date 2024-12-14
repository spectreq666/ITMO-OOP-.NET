using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Contracts.Accounts;

public interface IAdminAccountService
{
    LoginResult Login(string password);

    void UpdateMasterKey(string masterKey);

    Account? GetAccountByNumber(int number);

    decimal GetBalanceByNumber(int number);
}