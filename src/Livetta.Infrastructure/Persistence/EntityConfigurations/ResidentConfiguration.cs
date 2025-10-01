using Livetta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
{
    public void Configure(EntityTypeBuilder<Resident> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.Apartments)
            .WithOne(x => x.Resident)
            .OnDelete(DeleteBehavior.Cascade);
    }
}