using Lab5.Application.Models.Transactions;

namespace Lab5.Application.Contracts.Transactions;

public interface ITransactionService
{
    void CreateTransaction(Guid accountId, decimal amount, TransactionType transactionType);

    IEnumerable<Transaction>? GetTransactionHistory(Guid accountId);
}