using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities.Messaging;

public class ChatResident : Entity
{
    public Guid ResidentId { get; set; }
    public Resident Resident { get; set; }
    
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
}