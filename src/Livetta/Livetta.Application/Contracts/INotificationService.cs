using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;

namespace Livetta.Application.Contracts;

public interface INotificationService
{
    /// <summary>
    /// Уведомляет пользователей <see cref="usersId"/> о том, что создан новый чат с их участием
    /// или же их добавили в существующий чат.
    /// </summary>
    Task OnChatCreationAsync(Guid[] usersId, ChatReadDto readDto);
    
    /// <summary>
    /// Уведомляет пользователей <see cref="usersId"/> о том, что их изгнали из чата
    /// или же чат был удален.
    /// </summary>
    Task OnChatDeletionAsync(Guid[] usersId, Guid chatId);
    
    /// <summary>
    /// Уведомляет участников чата <see cref="chatId"/> о новом сообщении <see cref="readDto"/>.
    /// </summary>
    Task OnChatMessageAsync(Guid chatId, MessageReadDto readDto);
}