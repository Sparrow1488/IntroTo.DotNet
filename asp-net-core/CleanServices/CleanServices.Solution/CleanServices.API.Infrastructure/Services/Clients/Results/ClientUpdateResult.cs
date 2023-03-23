using CleanServices.API.Infrastructure.Services.Results;
using CleanServices.Models.Clients;

namespace CleanServices.API.Infrastructure.Services.Clients.Results;

public class ClientUpdateResult : Result<Client>
{
    public ClientUpdateResult(Exception ex) : base(ex)
    {
    }

    public ClientUpdateResult(Client entity) : base(entity)
    {
    }
}