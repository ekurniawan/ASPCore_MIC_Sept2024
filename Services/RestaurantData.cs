using System;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.Services;

public class RestaurantData : IRestaurantData
{
    private List<Restaurant> _restaurants;
    public RestaurantData()
    {
        _restaurants = new List<Restaurant>
        {
            new Restaurant { Id = 1, Name = "Bakmi Pele" },
            new Restaurant { Id = 2, Name = "Sate Klathak Pak Pong" },
            new Restaurant { Id = 3, Name = "Bakmi Mbah Hadi" },
            new Restaurant { Id = 4, Name = "Gudeg Sagan" },
            new Restaurant { Id = 5, Name = "Gudeg Bu Tjitro" }
        };
    }

    public Restaurant Add(Restaurant newRestaurant)
    {
        newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
        _restaurants.Add(newRestaurant);
        return newRestaurant;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Restaurant Get(int id)
    {
        Restaurant restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
        if (restaurant != null)
        {
            return restaurant;
        }
        else
        {
            throw new Exception("Restaurant not found");
        }
    }

    public IEnumerable<Restaurant> GetAll()
    {
        return _restaurants.OrderBy(r => r.Name);
    }

    public Restaurant Update(Restaurant updatedRestaurant)
    {
        throw new NotImplementedException();
    }
}
