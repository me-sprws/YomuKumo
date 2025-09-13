using Livetta.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livetta.Infrastructure.Persistence.EntityConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).IsRequired().HasMaxLength(100);

        builder
            .HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x => x.Resident)
            .WithMany()
            .HasForeignKey(x => x.ResidentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}