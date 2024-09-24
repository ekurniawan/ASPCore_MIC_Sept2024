using System;
using ASPCoreHOL.Models;
using System.Data.SqlClient;

namespace ASPCoreHOL.Services;

public class RestaurantADO : IRestaurantData
{
    private string? _connectionString;
    public RestaurantADO(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public Restaurant Add(Restaurant newRestaurant)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Restaurant Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Restaurant> GetAll()
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string strSql = @"SELECT Id, Name FROM Restaurants Order By Name asc";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Restaurant restaurant = new Restaurant();
                        restaurant.Id = Convert.ToInt32(dr["Id"]);
                        restaurant.Name = dr["Name"].ToString();
                        restaurants.Add(restaurant);
                    }
                    dr.Close();
                }
            }
            return restaurants;
        }
    }

    public Restaurant Update(Restaurant updatedRestaurant)
    {
        throw new NotImplementedException();
    }
}
