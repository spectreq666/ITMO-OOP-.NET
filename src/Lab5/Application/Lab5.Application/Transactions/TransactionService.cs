using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Transactions;

namespace Lab5.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public void CreateTransaction(Guid accountId, decimal amount, TransactionType transactionType)
    {
        Account? account = _accountRepository.GetById(accountId);
        if (account == null)
        {
            throw new InvalidOperationException($"Account with ID {accountId} not found.");
        }

        if (transactionType == TransactionType.Withdrawal && account.Balance < amount)
        {
            throw new InvalidOperationException("Not enough money");
        }

        var transaction = new Transaction(Guid.NewGuid(), accountId, amount, transactionType, DateTime.UtcNow);
        _transactionRepository.Save(transaction);

        if (transactionType == TransactionType.Withdrawal)
        {
            account.Balance -= amount;
        }
        else if (transactionType == TransactionType.Deposit)
        {
            account.Balance += amount;
        }

        _accountRepository.Save(account);
    }

    public IEnumerable<Transaction>? GetTransactionHistory(Guid accountId)
    {
        Account? account = _accountRepository.GetById(accountId);
        if (account == null)
        {
            throw new InvalidOperationException($"Account with ID {accountId} not found.");
        }

        return _transactionRepository.GetTransactionsByAccountId(accountId);
    }
}