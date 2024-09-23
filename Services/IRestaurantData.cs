using System;
using ASPCoreHOL.Models;
namespace ASPCoreHOL.Services;

public interface IRestaurantData
{
    IEnumerable<Restaurant> GetAll();
    Restaurant Get(int id);
    Restaurant Add(Restaurant newRestaurant);
    Restaurant Update(Restaurant updatedRestaurant);
    void Delete(int id);
}
