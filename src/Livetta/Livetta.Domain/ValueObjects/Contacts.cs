using Livetta.Domain.Contracts;

namespace Livetta.Domain.ValueObjects;

public record struct Contacts(string? FirstName, string? LastName, Phone? Phone) : IContacts;