using Livetta.Application.DTO.Messages;
using Livetta.Application.DTO.Residents;

namespace Livetta.Application.DTO.Chats;

public record ChatReadDto(Guid Id, ResidentReadShortDto[] Residents, MessageReadDto? LastMessage);