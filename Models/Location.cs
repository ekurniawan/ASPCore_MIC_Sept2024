using System;

namespace ASPCoreHOL.Models;

public class Location
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public ICollection<Restaurant> Restaurants { get; set; } = null!;
}
