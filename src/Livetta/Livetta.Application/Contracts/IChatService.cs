using Livetta.Application.DTO.Chats;

namespace Livetta.Application.Contracts;

public interface IChatService
{
    Task<ChatReadDto> CreateAsync(Guid residentId, ChatCreateDto request);
    Task<ChatReadDto[]> GetAllAsync(Guid residentId);
}