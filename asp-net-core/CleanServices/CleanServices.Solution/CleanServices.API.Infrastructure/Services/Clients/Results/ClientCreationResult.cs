using CleanServices.API.Infrastructure.Services.Results;
using CleanServices.Models.Clients;

namespace CleanServices.API.Infrastructure.Services.Clients.Results;

public class ClientCreationResult : Result<Client>
{
    public ClientCreationResult(Exception ex) : base(ex)
    {
    }

    public ClientCreationResult(Client entity) : base(entity)
    {
    }
}