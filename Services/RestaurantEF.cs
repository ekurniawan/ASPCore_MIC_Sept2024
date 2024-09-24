using System;
using ASPCoreHOL.Data;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.Services;

public class RestaurantEF : IRestaurantData
{
    private readonly ApplicationDbContext _db;
    public RestaurantEF(ApplicationDbContext db)
    {
        _db = db;
    }

    public Restaurant Add(Restaurant newRestaurant)
    {
        try
        {
            _db.Restaurants.Add(newRestaurant);
            _db.SaveChanges();
            return newRestaurant;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            var restaurant = Get(id);
            _db.Restaurants.Remove(restaurant);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Restaurant Get(int id)
    {
        var restaurant = _db.Restaurants.FirstOrDefault(r => r.Id == id);
        if (restaurant == null)
        {
            throw new Exception("Restaurant not found");
        }
        return restaurant;
    }

    public IEnumerable<Restaurant> GetAll()
    {
        var restaurants = _db.Restaurants.OrderBy(r => r.Name);
        return restaurants;
    }

    public IEnumerable<Restaurant> GetRestaurantsByName(string name)
    {
        var restaurants = _db.Restaurants.Where(r => r.Name.Contains(name)).OrderBy(r => r.Name);
        return restaurants;
    }

    public Restaurant Update(Restaurant updatedRestaurant)
    {
        try
        {
            var restaurant = Get(updatedRestaurant.Id);
            restaurant.Name = updatedRestaurant.Name;
            _db.SaveChanges();

            return restaurant;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
