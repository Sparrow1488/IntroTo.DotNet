namespace CleanServices.API.Infrastructure.Services.Hashers;

public interface IHasher
{
    string HashPassword(string password);
    bool ComparePasswords(string password, string passwordHash);
}