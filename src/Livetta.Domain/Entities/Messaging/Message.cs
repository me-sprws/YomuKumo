using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities.Messaging;

public class Message : Entity
{
    public string Text { get; set; }

    public Guid? ResidentId { get; set; }
    public Resident? Resident { get; set; }
    
    public Guid ChatId { get; set; }
    public Chat? Chat { get; set; }
}