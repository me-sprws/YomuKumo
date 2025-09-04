using Livetta.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class ContactsConfiguration : IEntityTypeConfiguration<ContactsEntity>
{
    public void Configure(EntityTypeBuilder<ContactsEntity> builder)
    {
        builder.ToTable("contacts");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FirstName).HasMaxLength(30);
        builder.Property(x => x.LastName).HasMaxLength(30);
        builder.Property(x => x.Phone).HasMaxLength(20);
        
        builder
            .HasOne(x => x.Resident)
            .WithOne(x => x.Contacts)
            .HasForeignKey<ContactsEntity>(x => x.ResidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}