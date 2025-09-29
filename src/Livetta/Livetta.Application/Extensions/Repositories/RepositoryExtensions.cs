using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Extensions.Repositories;

public static class RepositoryExtensions
{
    public static async Task<IReadOnlyList<ChatResident>?> GetChatMembersAsync(this IChatRepository chatRepository, Guid chatId)
    {
        var getChatMembers = chatRepository.Get(new(IncludeChatMembers: true, ChatId: chatId));
        var members = await chatRepository.SelectToListAsync(getChatMembers, ch => ch.Residents);
        
        return members.FirstOrDefault()?.ToList().AsReadOnly();
    }
}