using Livetta.Domain.Contracts;
using Livetta.Domain.Entities.Messaging;

namespace Livetta.Domain.Repositories;

public record GetMessageOptions(
    bool OrderByCreation = false,
    Guid? ChatId = null,
    bool AsNoTracking = false
);

public interface IMessageRepository : IRepository<Message>
{
    IQueryable<Message> Get(GetMessageOptions options);
}