using System.Security.Claims;
using Livetta.Application.Authorization;
using Livetta.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Livetta.WebAPI.Extensions;

internal static class AuthorizationServiceExtensions
{
    public static Task<AuthorizationResult> AuthorizeChatMemberAsync(this IAuthorizationService auth, ClaimsPrincipal user, ChatMemberAuthContext resource)
    {
        return auth.AuthorizeAsync(user, resource, LivettaPolicy.Messaging.ChatMember);
    }
}