using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Accounts;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly NpgsqlConnection _connection;

    public AccountRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public Account? GetById(Guid accountId)
    {
        var command = new NpgsqlCommand(
            "SELECT account_id, account_number, pin_hash, balance FROM accounts WHERE account_id = @accountId",
            _connection);
        command.Parameters.AddWithValue("@accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Account(
            reader.GetGuid(0),
            reader.GetInt32(1),
            reader.GetString(2),
            reader.GetDecimal(3));
    }

    public Account? GetByNumber(int accountNumber)
    {
        var command = new NpgsqlCommand(
            "SELECT account_id, account_number, pin_hash, balance FROM accounts WHERE account_number = @accountNumber",
            _connection);
        command.Parameters.AddWithValue("@accountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Account(
            reader.GetGuid(0),
            reader.GetInt32(1),
            reader.GetString(2),
            reader.GetDecimal(3));
    }

    public void Save(Account account)
    {
        var command = new NpgsqlCommand(
            @"INSERT INTO accounts (account_id, account_number, pin_hash, balance)
              VALUES (@accountId, @accountNumber, @pinHash, @balance)
              ON CONFLICT (account_id) DO UPDATE 
              SET account_number = @accountNumber, pin_hash = @pinHash, balance = @balance",
            _connection);

        command.Parameters.AddWithValue("@accountId", account.Id);
        command.Parameters.AddWithValue("@accountNumber", account.Number);
        command.Parameters.AddWithValue("@pinHash", account.PinHash);
        command.Parameters.AddWithValue("@balance", account.Balance);

        command.ExecuteNonQuery();
    }
}
