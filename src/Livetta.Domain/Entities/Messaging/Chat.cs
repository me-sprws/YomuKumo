using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities.Messaging;

public class Chat : Entity
{
    public ICollection<ChatResident> Residents { get; set; } = new List<ChatResident>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}