using System.Security.Cryptography;
using System.Text;

namespace CleanServices.API.Infrastructure.Services.Hashers;

public class Sha256Hasher : IHasher
{
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var secretBytes = Encoding.UTF8.GetBytes(password);
        var secretHash = sha256.ComputeHash(secretBytes);
        return Convert.ToHexString(secretHash);
    }

    public bool ComparePasswords(string password, string passwordHash)
    {
        var hashedPassword = HashPassword(password);
        return hashedPassword == passwordHash;
    }
}