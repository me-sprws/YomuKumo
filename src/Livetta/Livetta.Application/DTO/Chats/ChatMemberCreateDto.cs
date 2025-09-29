using Livetta.Domain.Entities.Messaging;

namespace Livetta.Application.DTO.Chats;

public record ChatMemberCreateDto(Guid ResidentId, ChatMemberKind MemberKind);