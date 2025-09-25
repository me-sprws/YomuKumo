using Livetta.Domain.Contracts;
using Livetta.Domain.Entities.Messaging;

namespace Livetta.Domain.Repositories;

public record GetChatOptions(
    Guid ChatId = default,
    Guid ResidentId = default,
    bool IncludeResidents = false,
    bool IncludeLastMessage = false,
    bool AsNoTracking = false    
);

public interface IChatRepository : IRepository<Chat>
{
    IQueryable<Chat> Get(GetChatOptions options);

    Task<bool> IsChatMemberAsync(Guid residentId, Guid chatId);
}