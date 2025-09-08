using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Livetta.Infrastructure.Persistence;

public sealed class LivettaDbContext : DbContext, IUnitOfWork
{
    IDbContextTransaction _transaction;

    public LivettaDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Contacts> Contacts => Set<Contacts>();
    public DbSet<Resident> Residents => Set<Resident>();
    public DbSet<Apartment> Apartments => Set<Apartment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(LivettaDbContext).Assembly);
    }

    public async Task<IDisposable> BeginTransactionAsync(CancellationToken ctk = default)
    {
        return _transaction = await Database.BeginTransactionAsync(ctk);
    }

    public Task CommitTransactionAsync(CancellationToken ctk = default)
    {
        return _transaction.CommitAsync(ctk);
    }

    #region SaveChange Overrides

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateTrackable();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateTrackable();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTrackable();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTrackable();
        return base.SaveChangesAsync(cancellationToken);
    }

    #endregion

    void UpdateTrackable()
    {
        foreach (var trackableEntity in ChangeTracker.Entries().Where(x => x.Entity is ITrackable))
        {
            var trackable = (ITrackable) trackableEntity.Entity;
            
            if (trackableEntity.State is EntityState.Added)
                trackable.CreatedAt = DateTimeOffset.UtcNow;
            
            if (trackableEntity.State is EntityState.Modified)
                trackable.UpdatedAt = DateTimeOffset.Now;

            trackable.RowVersion = Guid.NewGuid().ToByteArray();
        }
    }
}