using Livetta.Application.DTO.Chats;

namespace Livetta.Application.Contracts;

public interface IChatService
{
    Task CreateChatMemberAsync(Guid chatId, ChatMemberCreateDto request);
    Task DeleteChatMemberAsync(Guid chatId, ChatMemberDeleteDto request);
    Task<ChatReadDto> CreateAsync(Guid residentId, ChatCreateDto request);
    Task DeleteAsync(Guid chatId);
    Task<ChatReadDto[]> GetAllAsync(Guid residentId);
}