using Livetta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address).HasMaxLength(150);
        builder.Property(x => x.Area).HasMaxLength(500);
        builder.Property(x => x.Floor).HasMaxLength(100);
        builder.Property(x => x.Room).HasMaxLength(20);

        builder.HasMany(x => x.Residents)
            .WithOne(x => x.Apartment)
            .OnDelete(DeleteBehavior.Cascade);
    }
}