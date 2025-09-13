using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class MessageRepository(LivettaDbContext dbContext) : Repository<Message>(dbContext), IMessageRepository
{
    public IQueryable<Message> Get(GetMessageOptions options)
    {
        var builder = QueryableSet;

        if (options.OrderByCreation)
        {
            builder = builder.OrderByDescending(x => x.CreatedAt);
        }

        if (options.ChatId is { } chatId)
        {
            builder = builder.Where(x => x.ChatId == chatId);
        }
        
        if (options.AsNoTracking)
        {
            builder.AsNoTracking();
        }

        return builder;
    }
}