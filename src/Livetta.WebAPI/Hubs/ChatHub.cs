using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Hubs;

[Authorize]
public sealed class ChatHub : Hub<IClientProxy>
{
    public override Task OnConnectedAsync()
    {
        return Task.CompletedTask;
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return Task.CompletedTask;
    }
}