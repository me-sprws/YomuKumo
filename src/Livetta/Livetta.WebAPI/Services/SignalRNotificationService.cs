using Livetta.Application.Contracts;
using Livetta.Application.DTO.Messages;
using Livetta.Infrastructure.Services;
using Livetta.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Services;

public sealed class SignalRNotificationService(IHubContext<ChatHub> hubContext) : INotificationService
{
    // public async Task OnChatCreatedAsync(Guid chatId, ChatReadDto readDto)
    // {
    //     await hubContext.Clients
    //         .Group(SignalRGroupBuilder.GetChatGroup(chatId))
    //         .SendAsync("OnChatCreated", readDto);
    // }

    public async Task OnChatMessageAsync(Guid chatId, MessageReadDto readDto)
    {
        await hubContext.Clients
            .Group(SignalRGroupBuilder.GetChatGroup(chatId))
            .SendAsync("OnChatMessage", readDto);
    }
}