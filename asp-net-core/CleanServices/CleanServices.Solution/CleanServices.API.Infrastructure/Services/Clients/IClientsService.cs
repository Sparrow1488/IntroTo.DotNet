using CleanServices.API.Infrastructure.Services.Clients.Results;
using CleanServices.Models.Clients;

namespace CleanServices.API.Infrastructure.Services.Clients;

public interface IClientsService
{
    Task<Client?> GetByIdAsync(int id);
    Task<ClientCreationResult> CreateAsync(string login, string password, string? email);
    Task<ClientUpdateResult> UpdateAsync(Client client);
}