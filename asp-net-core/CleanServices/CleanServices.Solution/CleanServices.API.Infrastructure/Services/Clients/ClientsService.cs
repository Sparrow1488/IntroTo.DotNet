using CleanServices.API.Infrastructure.Extensions;
using CleanServices.API.Infrastructure.Services.Clients.Results;
using CleanServices.Data.Infrastructure.Contexts;
using CleanServices.Models.Clients;
using CleanServices.Models.Clients.Credentials;
using CleanServices.Validators.Models.Credentials.Password;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanServices.API.Infrastructure.Services.Clients;

public class ClientsService : IClientsService
{
    private readonly IValidator<Client> _validator;
    private readonly IPasswordValidator _passwordValidator;
    private readonly AppDbContext _context;

    public ClientsService(
        IValidator<Client> validator,
        IPasswordValidator passwordValidator,
        AppDbContext context)
    {
        _validator = validator;
        _passwordValidator = passwordValidator;
        _context = context;
    }

    public Task<Client?> GetByIdAsync(int id)
    {
        return _context.Clients
            .Include(x => x.Info)
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ClientCreationResult> CreateAsync(
        Client client, string login, string password, string? email)
    {
        var clientValidResult = await _validator.ValidateAsync(client);
        if (!clientValidResult.IsValid)
            return new ClientCreationResult(clientValidResult.ToException());

        var passwordValidResult = await _passwordValidator.ValidateAsync(password);
        if (!passwordValidResult.IsValid)
            return new ClientCreationResult(passwordValidResult.ToException());

        client.Credentials = new ClientCredentials
        {
            Login = login,
            HashedPassword = "hashed_" + password,
            Email = email
        };
            
        await _context.AddAsync(client);
        await _context.SaveChangesAsync();

        return new ClientCreationResult(client);
    }
}