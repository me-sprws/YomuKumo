using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Resident : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Contacts Contacts { get; set; }
    
    public ICollection<ResidentApartment> Apartments { get; set; } = new List<ResidentApartment>();
}