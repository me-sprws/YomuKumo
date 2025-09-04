using Livetta.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ResidentConfiguration : IEntityTypeConfiguration<ResidentEntity>
{
    public void Configure(EntityTypeBuilder<ResidentEntity> builder)
    {
        builder.ToTable("residents");
        builder.HasKey(x => x.Id);
    }
}