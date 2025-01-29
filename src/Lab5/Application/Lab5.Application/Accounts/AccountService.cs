using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Utils.Cryptography;

namespace Lab5.Application.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly CurrentAccount _currentAccount;

    public AccountService(IAccountRepository accountRepository, CurrentAccount currentAccount)
    {
        _accountRepository = accountRepository;
        _currentAccount = currentAccount;
    }

    public LoginResult CreateAccount(int number, string password)
    {
        Account? existAccount = _accountRepository.GetByNumber(number);

        if (existAccount != null)
        {
            return new LoginResult.Failed();
        }

        string passwordHash = PasswordEncryptor.HashPassword(password);

        var account = new Account(Guid.NewGuid(), number, passwordHash, 0);
        _accountRepository.Save(account);

        _currentAccount.Account = account;
        return new LoginResult.Success();
    }

    public LoginResult Login(int number, string password)
    {
        Account? account = _accountRepository.GetByNumber(number);

        if (account is null)
        {
            return new LoginResult.Failed();
        }

        string passwordHash = PasswordEncryptor.HashPassword(password);

        if (account.PinHash != passwordHash)
        {
            return new LoginResult.Failed();
        }

        _currentAccount.Account = account;
        return new LoginResult.Success();
    }

    public decimal GetBalance(Guid accountId)
    {
        Account? account = _accountRepository.GetById(accountId);
        if (account == null)
            throw new ArgumentException("Account not found.");

        return account.Balance;
    }
}