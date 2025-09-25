using Livetta.Application.DTO.Messages;

namespace Livetta.Application.Contracts;

public interface INotificationService
{
    // Task OnChatCreatedAsync(Guid chatId, ChatReadDto readDto);
    Task OnChatMessageAsync(Guid chatId, MessageReadDto readDto);
}