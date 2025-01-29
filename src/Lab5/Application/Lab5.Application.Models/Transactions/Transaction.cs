namespace Lab5.Application.Models.Transactions;

public class Transaction
{
    public Transaction(Guid transactionId, Guid accountId, decimal amount, TransactionType type, DateTime date)
    {
        Id = transactionId;
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Timestamp = date;
    }

    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public DateTime Timestamp { get; set; }
}