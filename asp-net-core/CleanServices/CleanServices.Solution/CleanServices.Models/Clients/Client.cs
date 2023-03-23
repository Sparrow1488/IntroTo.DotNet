using CleanServices.Models.Clients.Credentials;
using CleanServices.Models.Clients.Info;
using CleanServices.Models.Clients.Profile;
using CleanServices.Models.Identity;

namespace CleanServices.Models.Clients;

public class Client : Entity
{
    public ClientInfo? Info { get; set; }
    public ClientCredentials Credentials { get; set; }
    public ClientProfile? Profile { get; set; }
}