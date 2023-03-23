using CleanServices.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace CleanServices.Data.Infrastructure.Contexts;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
}