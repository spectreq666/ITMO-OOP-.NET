using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Accounts;

public class AdminAccountService : IAdminAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAdminRepository _adminRepository;

    public AdminAccountService(IAccountRepository accountRepository, IAdminRepository adminRepository)
    {
        _accountRepository = accountRepository;
        _adminRepository = adminRepository;
    }

    public LoginResult Login(string password)
    {
        if (password != _adminRepository.GetCurrentMasterKey())
        {
            return new LoginResult.Failed();
        }

        return new LoginResult.Success();
    }

    public void UpdateMasterKey(string masterKey)
    {
        _adminRepository.SaveCurrentMasterKey(masterKey);
    }

    public Account? GetAccountByNumber(int number)
    {
        Account? account = _accountRepository.GetByNumber(number);
        if (account == null)
            throw new ArgumentException("Account not found.");

        return account;
    }

    public decimal GetBalanceByNumber(int number)
    {
        Account? account = _accountRepository.GetByNumber(number);
        if (account == null)
            throw new ArgumentException("Account not found.");

        return account.Balance;
    }
}