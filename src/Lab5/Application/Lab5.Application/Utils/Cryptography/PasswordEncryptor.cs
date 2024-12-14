using System.Security.Cryptography;
using System.Text;

namespace Lab5.Application.Utils.Cryptography;

public class PasswordEncryptor
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = sha256.ComputeHash(passwordBytes);

        return BitConverter.ToString(hashBytes)
            .Replace("-", string.Empty, StringComparison.Ordinal)
            .ToLowerInvariant();
    }
}