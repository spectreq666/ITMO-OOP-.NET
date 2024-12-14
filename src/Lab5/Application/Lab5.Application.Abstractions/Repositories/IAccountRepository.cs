using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Account? GetById(Guid accountId);

    Account? GetByNumber(int accountNumber);

    void Save(Account account);
}