namespace Livetta.Domain.Contracts;

public class Entity : IHasKey<Guid>, ITrackable
{
    public Guid Id { get; set; }
    public byte[] RowVersion { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}