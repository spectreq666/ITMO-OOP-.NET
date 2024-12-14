using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Transactions;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly NpgsqlConnection _connection;

    public TransactionRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public void Save(Transaction transaction)
    {
        var command = new NpgsqlCommand(
            @"INSERT INTO transactions (transaction_id, account_id, amount, type, timestamp)
                  VALUES (@transactionId, @accountId, @amount, @type, @timestamp)",
            _connection);

        command.Parameters.AddWithValue("@transactionId", transaction.Id);
        command.Parameters.AddWithValue("@accountId", transaction.AccountId);
        command.Parameters.AddWithValue("@amount", transaction.Amount);
        command.Parameters.AddWithValue("@type", transaction.Type.ToString());
        command.Parameters.AddWithValue("@timestamp", transaction.Timestamp);

        command.ExecuteNonQuery();
    }

    public IEnumerable<Transaction>? GetTransactionsByAccountId(Guid accountId)
    {
        var transactions = new List<Transaction>();

        var command = new NpgsqlCommand(
            @"SELECT transaction_id, account_id, amount, type, timestamp 
          FROM transactions WHERE account_id = @accountId",
            _connection);

        command.Parameters.AddWithValue("@accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var transaction = new Transaction(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetDecimal(2),
                Enum.Parse<TransactionType>(reader.GetString(3)),
                reader.GetDateTime(4));

            transactions.Add(transaction);
        }

        return transactions.Count > 0 ? transactions : null;
    }
}