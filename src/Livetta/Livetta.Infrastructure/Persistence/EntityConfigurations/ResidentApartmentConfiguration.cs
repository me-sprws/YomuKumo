using Livetta.Domain.Entities;
using Livetta.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ResidentApartmentConfiguration : IEntityTypeConfiguration<ResidentApartment>
{
    public void Configure(EntityTypeBuilder<ResidentApartment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Role)
            .HasDefaultValue(ResidentRole.Occupant)
            .HasMaxLength(10);

        builder.HasOne(x => x.Apartment)
            .WithMany(x => x.Residents)
            .HasForeignKey(x => x.ApartmentId);
        
        builder.HasOne(x => x.Resident)
            .WithMany(x => x.Apartments)
            .HasForeignKey(x => x.ResidentId);
    }
}