using Lab5.Application.Abstractions.Repositories;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly NpgsqlConnection _connection;

    public AdminRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public string GetCurrentMasterKey()
    {
        var command = new NpgsqlCommand("select master_key from admins", _connection);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetString(0);
        }

        throw new InvalidOperationException("No master key found in the database.");
    }

    public void SaveCurrentMasterKey(string masterKey)
    {
        var command = new NpgsqlCommand("update admins set master_key=@masterKey", _connection);

        command.Parameters.AddWithValue("@masterKey", masterKey);
        command.ExecuteNonQuery();
    }
}