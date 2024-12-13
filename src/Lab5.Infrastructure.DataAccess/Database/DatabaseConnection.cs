using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Database;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection Init()
    {
        var connection = new NpgsqlConnection(_connectionString);
        try
        {
            connection.Open();
            CreateTables(connection);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при инициализации базы данных {ex.Message}");
        }

        return connection;
    }

    private void CreateTables(NpgsqlConnection connection)
    {
        const string createAccountsTableCommand = @"
            CREATE TABLE IF NOT EXISTS accounts (
                account_id UUID PRIMARY KEY,
                account_number INTEGER NOT NULL,
                pin_hash TEXT NOT NULL,
                balance DECIMAL(10, 2) DEFAULT 0
            );
        ";

        const string createTransactionsTableCommand = @"
            CREATE TABLE IF NOT EXISTS transactions (
                transaction_id UUID PRIMARY KEY,
                account_id UUID REFERENCES accounts(account_id) ON DELETE CASCADE,
                amount DECIMAL(10, 2) NOT NULL,
                type TEXT NOT NULL,
                timestamp TIMESTAMP NOT NULL
            );
        ";

        const string createAdminPasswordTable = @"
            CREATE TABLE IF NOT EXISTS admins (
                master_key TEXT NOT NULL DEFAULT 'MasterKey'
            );
            INSERT INTO admins (master_key) VALUES ('MASTERKEY')
        ";

        try
        {
            using var command = new NpgsqlCommand(createAccountsTableCommand, connection);
            command.ExecuteNonQuery();

            using var command2 = new NpgsqlCommand(createTransactionsTableCommand, connection);
            command2.ExecuteNonQuery();

            using var command3 = new NpgsqlCommand(createAdminPasswordTable, connection);
            command3.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании таблиц: {ex.Message}");
        }
    }
}