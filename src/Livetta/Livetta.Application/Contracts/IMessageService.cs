using Livetta.Application.DTO.Messages;

namespace Livetta.Application.Contracts;

public interface IMessageService
{
    Task<MessageReadDto[]> GetMessagesAsync(Guid chatId, int take, int offset);
    Task<MessageReadDto> CreateAsync(Guid chatId, Guid residentId, MessageCreateDto request);
}