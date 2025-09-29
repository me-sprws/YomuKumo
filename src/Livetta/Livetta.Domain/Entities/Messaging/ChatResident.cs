using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities.Messaging;

public class ChatResident : Entity
{
    public ChatMemberKind MemberKind { get; set; }
    
    public Guid ResidentId { get; set; }
    public Resident Resident { get; set; }
    
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
}

public enum ChatMemberKind
{
    ReadOnly = 0,
    Member = 1,
    Owner = 2
}