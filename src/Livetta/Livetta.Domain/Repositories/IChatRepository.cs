using Livetta.Domain.Contracts;
using Livetta.Domain.Entities.Messaging;

namespace Livetta.Domain.Repositories;

public record GetChatOptions(
    bool IncludeResidents = false,
    bool IncludeFirstMessage = false,
    bool AsNoTracking = false    
);

public interface IChatRepository : IRepository<Chat>
{
    IQueryable<Chat> Get(GetChatOptions options);
}