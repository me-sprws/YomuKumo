using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Apartment : Entity
{
    public string Address { get; set; }
    public int Room { get; set; }
    public int Floor { get; set; }
    public double Area { get; set; }
    public ICollection<ResidentApartment> Residents { get; } = new List<ResidentApartment>();
}