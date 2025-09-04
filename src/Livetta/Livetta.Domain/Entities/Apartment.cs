using DotNext.Collections.Generic;
using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Apartment : Entity, IApartment
{
    readonly List<IResident> _residents;

    public Apartment(string address, int room, int floor, double area, IEnumerable<IResident> residents)
    {
        Address = address;
        Room = room;
        Floor = floor;
        Area = area;
        _residents = residents.ToList();
    }

    public string Address { get; }
    public int Room { get; }
    public int Floor { get; }
    public double Area { get; }
    public IReadOnlyCollection<IResident> Residents => _residents.AsReadOnly();
    
    public void AddResident(IResident resident)
    {
        if (!_residents.Contains(resident))
            _residents.Add(resident);
        
        resident.AddApartment(this);
    }

    public int RemoveResidentAll(Predicate<IResident> match)
    {
        var matched = _residents.Where(match.Invoke);
        matched.ForEach(r => r.RemoveApartment(this));
        
        return _residents.RemoveAll(match);
    }
}