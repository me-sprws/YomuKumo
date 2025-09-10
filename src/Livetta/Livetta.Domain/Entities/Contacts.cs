using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Contacts : Entity
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public Guid ResidentId { get; set; }
}