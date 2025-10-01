using System.ComponentModel.DataAnnotations;

namespace Livetta.Domain.Contracts;

public interface ITrackable
{
    [Timestamp]
    byte[] RowVersion { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}