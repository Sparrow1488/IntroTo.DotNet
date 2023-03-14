using CleanServices.API.Infrastructure.Services.Clients.Results;
using CleanServices.Models.Clients;

namespace CleanServices.API.Infrastructure.Services.Clients;

public interface IClientsService
{
    Task<Client?> GetByIdAsync(int id);
    Task<ClientCreationResult> CreateAsync(Client client, string login, string password, string? email);
}