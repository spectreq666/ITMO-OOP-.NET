using Lab5.Application.Models.Transactions;

namespace Lab5.Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    void Save(Transaction transaction);

    public IEnumerable<Transaction>? GetTransactionsByAccountId(Guid accountId);
}