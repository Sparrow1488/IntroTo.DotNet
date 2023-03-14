using CleanServices.Models.Identity;

namespace CleanServices.Models.Clients.Info;

public class ClientInfo : Entity
{
    public string Name { get; set; }
    public int Age { get; set; }
}