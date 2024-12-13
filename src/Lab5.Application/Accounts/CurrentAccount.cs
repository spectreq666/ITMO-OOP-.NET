using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Accounts;

public class CurrentAccount : ICurrentAccountService
{
    public Account? Account { get; set; }
}