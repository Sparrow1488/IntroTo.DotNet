using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using CleanServices.API.Infrastructure.Extensions;
using CleanServices.API.Infrastructure.Services.Clients.Results;
using CleanServices.API.Infrastructure.Services.Hashers;
using CleanServices.Data.Infrastructure.Contexts;
using CleanServices.Models.Clients;
using CleanServices.Models.Clients.Credentials;
using CleanServices.Validators.Models.Credentials.Password;
using Microsoft.EntityFrameworkCore;

namespace CleanServices.API.Infrastructure.Services.Clients;

public class ClientsService : IClientsService
{
    private readonly IHasher _hasher;
    private readonly IPasswordValidator _passwordValidator;
    private readonly AppDbContext _context;

    public ClientsService(
        IHasher hasher,
        AppDbContext context,
        IPasswordValidator passwordValidator)
    {
        _hasher = hasher;
        _context = context;
        _passwordValidator = passwordValidator;
    }

    public Task<Client?> GetByIdAsync(int id)
    {
        return _context.Clients
            .Include(x => x.Info)
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ClientCreationResult> CreateAsync(string login, string password, string? email)
    {
        var passwordValidResult = await _passwordValidator.ValidateAsync(password);
        if (!passwordValidResult.IsValid)
            return new ClientCreationResult(passwordValidResult.ToException());

        var hashedPassword = _hasher.HashPassword(password);

        var client = new Client
        {
            Credentials = new ClientCredentials
            {
                Login = login,
                HashedPassword = hashedPassword,
                Email = email
            }
        };
            
        await _context.AddAsync(client);
        await _context.SaveChangesAsync();

        return new ClientCreationResult(client);
    }

    public async Task<ClientUpdateResult> UpdateAsync(Client client)
    {
        var clientExists = await _context.Clients.AnyAsync(x => x.Id == client.Id);

        if (!clientExists)
        {
            var ex = new NotFoundException("Client not found", client.Id, "Client");
            return new ClientUpdateResult(ex);
        }

        _context.Update(client);
        await _context.SaveChangesAsync();

        return new ClientUpdateResult(client);
    }
}