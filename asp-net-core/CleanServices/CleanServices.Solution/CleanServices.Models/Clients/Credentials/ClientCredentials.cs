using CleanServices.Models.Identity;

namespace CleanServices.Models.Clients.Credentials;

public class ClientCredentials : Entity
{
    public string Login { get; set; }
    public string HashedPassword { get; set; }
    public string? Email { get; set; }
}