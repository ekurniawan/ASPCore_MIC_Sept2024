using System;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.Services;

public interface ILocationData
{
    IEnumerable<Location> GetAll();
    Location Get(int id);
    Location Add(Location newLocation);
    Location Update(Location updatedLocation);
    void Delete(int id);
    IEnumerable<Location> GetLocationsByName(string name);
}
