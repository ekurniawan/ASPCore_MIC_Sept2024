using System;
using ASPCoreHOL.Data;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.Services;

public class LocationData : ILocationData
{
    private readonly ApplicationDbContext _applicationDbContext;
    public LocationData(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Location Add(Location newLocation)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Location Get(int id)
    {
        var location = _applicationDbContext.Locations.FirstOrDefault(l => l.LocationId == id);
        if (location == null)
        {
            throw new Exception("Location not found");
        }
        return location;
    }

    public IEnumerable<Location> GetAll()
    {
        return _applicationDbContext.Locations;
    }

    public IEnumerable<Location> GetLocationsByName(string name)
    {
        throw new NotImplementedException();
    }

    public Location Update(Location updatedLocation)
    {
        throw new NotImplementedException();
    }
}
