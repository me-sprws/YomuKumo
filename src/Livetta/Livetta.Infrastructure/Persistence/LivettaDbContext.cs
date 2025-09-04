using Livetta.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence;

public sealed class LivettaDbContext : DbContext
{
    public LivettaDbContext(DbContextOptions options) : base(options)
    {
        Contacts = Set<ContactsEntity>();
        Residents = Set<ResidentEntity>();
    }

    public DbSet<ContactsEntity> Contacts { get; }
    public DbSet<ResidentEntity> Residents { get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(LivettaDbContext).Assembly);
    }
}