using System.Security.Claims;
using Livetta.Domain.Repositories;
using Livetta.WebAPI.Extensions;
using Livetta.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Hubs;

[Authorize]
public sealed class ChatHub(
    IAuthorizationService authorizationService,
    IChatRepository chatRepository
) : Hub<IClientProxy>
{
    public override async Task OnConnectedAsync()
    {
        if (!Guid.TryParse(Context.UserIdentifier, out var residentId)) 
            return;
        
        var chatList = await chatRepository.ToListAsync(chatRepository.Get(new(ResidentId: residentId)));

        foreach (var chat in chatList)
            await Groups.AddToGroupAsync(Context.ConnectionId, WebSocketChannelBuilder.GetChatGroup(chat.Id));
    }
    
    public async Task ConnectToChatGroup(Guid chatId)
    {
        if (Context.User is null)
            return;
        
        // TODO: Если пользователь получает сообщение о том, что его изгнали из чата, но при этом не отписался от
        // уведомлений о новых сообщениях OnChatMessage, то это брешь безопасности и требуется проверка,
        // состоит ли он в чате. Либо можно отсылать уведомления не группам, а Chat.Residents - участникам напрямую.

        if ((await authorizationService.AuthorizeChatMemberAsync(Context.User, new(chatId))).Succeeded)
            await Groups.AddToGroupAsync(Context.ConnectionId, WebSocketChannelBuilder.GetChatGroup(chatId));
    }
    
    public async Task DisconnectFromChatGroup(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, WebSocketChannelBuilder.GetChatGroup(chatId));
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return Task.CompletedTask;
    }
}