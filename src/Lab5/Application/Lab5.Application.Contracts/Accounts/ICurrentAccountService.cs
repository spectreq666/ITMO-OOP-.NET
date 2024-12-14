using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Contracts.Accounts;

public interface ICurrentAccountService
{
    Account? Account { get; }
}