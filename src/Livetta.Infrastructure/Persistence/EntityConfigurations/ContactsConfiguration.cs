using Livetta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ContactsConfiguration : IEntityTypeConfiguration<Contacts>
{
    public void Configure(EntityTypeBuilder<Contacts> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Phone).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(50);
        
        builder
            .HasOne<Resident>()
            .WithOne(x => x.Contacts)
            .HasForeignKey<Contacts>(x => x.ResidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}