using System.Security.Claims;
using Livetta.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Livetta.WebAPI.Authorization;

public record ChatMemberAuthContext(Guid ChatId, bool CheckOwner = false);

public record ChatMemberRequirement : IAuthorizationRequirement;

public sealed class ChatMemberAuthHandler(IChatRepository chatRepository) : AuthorizationHandler<ChatMemberRequirement, ChatMemberAuthContext>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ChatMemberRequirement requirement, ChatMemberAuthContext chat)
    {
        var residentIdStr = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(residentIdStr, out var residentId))
        {
            context.Fail();
        }
        
        // TODO: CheckOwner

        if (await chatRepository.IsChatMemberAsync(residentId, chat.ChatId))
            context.Succeed(requirement);
        else 
            context.Fail();
    }
}