using System;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.ViewModels;

public class RestaurantViewModel
{
    public IEnumerable<Restaurant> Restaurants { get; set; } = null!;
    public string? CurrentMessage { get; set; }
}
