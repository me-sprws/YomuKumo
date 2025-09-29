using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class ChatRepository(LivettaDbContext dbContext) : Repository<Chat>(dbContext), IChatRepository
{
    public IQueryable<Chat> Get(GetChatOptions options)
    {
        var builder = QueryableSet;

        if (options.IncludeChatMembers)
        {
            builder = builder.Include(x => x.Residents);
        }
        
        if (options.IncludeResidents)
        {
            builder = builder
                .Include(x => x.Residents)
                .ThenInclude(x => x.Resident)
                .ThenInclude(x => x.Contacts);
        }
        
        if (options.ChatId != default)
        {
            builder = builder.Where(ch => ch.Id == options.ChatId);
        }

        if (options.ResidentId != default)
        {
            builder = builder.Where(ch =>
                ch.Residents.Any(r => r.ResidentId == options.ResidentId));
        }
        
        if (options.IncludeLastMessage)
        {
            builder = builder.Include(x => x.Messages.OrderByDescending(m => m.CreatedAt).Take(1));
        }

        if (options.AsNoTracking)
        {
            builder.AsNoTracking();
        }

        return builder;
    }

    public Task<bool> IsChatMemberAsync(Guid residentId, Guid chatId)
    {
        return AnyAsync(QueryableSet.Where(
            ch => chatId == ch.Id && ch.Residents.Any(r => r.ResidentId == residentId)));
    }
}