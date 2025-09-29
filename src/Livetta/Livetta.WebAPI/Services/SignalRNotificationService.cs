using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Services;

public sealed class SignalRNotificationService(IHubContext<ChatHub> hubContext) : INotificationService
{
    public Task OnChatCreationAsync(Guid[] usersId, ChatReadDto readDto)
    {
        return hubContext.Clients
            .Users(usersId.Select(u => u.ToString()))
            .SendAsync("OnChatCreation", readDto);
    }

    public Task OnChatDeletionAsync(Guid[] usersId, Guid chatId)
    {
        return hubContext.Clients
            .Users(usersId.Select(u => u.ToString()))
            .SendAsync("OnChatDeletion", chatId);
    }

    public Task OnChatMessageAsync(Guid[] chatMembersId, MessageReadDto readDto)
    {
        return hubContext.Clients
            .Users(chatMembersId.Select(u => u.ToString()))
            .SendAsync("OnChatMessage", readDto);
    }
}