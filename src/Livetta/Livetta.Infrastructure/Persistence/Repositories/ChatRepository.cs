using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class ChatRepository(LivettaDbContext dbContext) : Repository<Chat>(dbContext), IChatRepository
{
    public IQueryable<Chat> Get(GetChatOptions options)
    {
        var builder = QueryableSet;

        if (options.IncludeResidents)
        {
            builder = builder
                .Include(x => x.Residents)
                .ThenInclude(x => x.Resident)
                .ThenInclude(x => x.Contacts);
        }
        
        if (options.IncludeFirstMessage)
        {
            builder = builder.Include(x => x.Messages.Take(1));
        }

        if (options.AsNoTracking)
        {
            builder.AsNoTracking();
        }

        return builder;
    }
}