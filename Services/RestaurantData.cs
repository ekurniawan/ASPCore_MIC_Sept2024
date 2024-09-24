using System;
using ASPCoreHOL.Models;
using Microsoft.AspNetCore.Mvc;

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
        var restaurant = Get(id);
        if (restaurant != null)
        {
            _restaurants.Remove(restaurant);
        }
        else
        {
            throw new Exception("Restaurant not found");
        }
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

    public IEnumerable<Restaurant> GetRestaurantsByName(string name)
    {
        throw new NotImplementedException();
    }

    public Restaurant Update(Restaurant updatedRestaurant)
    {
        var restaurant = Get(updatedRestaurant.Id);
        if (restaurant != null)
        {
            restaurant.Name = updatedRestaurant.Name;
            return restaurant;
        }
        else
        {
            throw new Exception("Restaurant not found");
        }
    }
}
