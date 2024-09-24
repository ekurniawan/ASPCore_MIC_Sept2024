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
        string strSql = @"INSERT INTO Restaurants (Name) VALUES (@Name);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", newRestaurant.Name);
                conn.Open();
                try
                {
                    newRestaurant.Id = (int)cmd.ExecuteScalar();
                    return newRestaurant;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public void Delete(int id)
    {
        try
        {
            var restaurant = Get(id);
            string strSql = @"DELETE FROM Restaurants WHERE Id = @Id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception(sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Restaurant Get(int id)
    {
        Restaurant restaurant = new Restaurant();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string strSql = @"SELECT Id, Name FROM Restaurants
                              WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    restaurant.Id = Convert.ToInt32(dr["Id"]);
                    restaurant.Name = dr["Name"].ToString();
                }
                else
                {
                    throw new Exception("Data tidak ditemukan");
                }
                dr.Close();
            }
            return restaurant;
        }
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

    public IEnumerable<Restaurant> GetRestaurantsByName(string name)
    {
        string strSql = @"SELECT Id, Name FROM Restaurants WHERE Name LIKE @Name";
        List<Restaurant> restaurants = new List<Restaurant>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
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
        try
        {
            var restaurant = Get(updatedRestaurant.Id);
            string strSql = @"UPDATE Restaurants SET Name = @Name WHERE Id = @Id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", updatedRestaurant.Name);
                    cmd.Parameters.AddWithValue("@Id", updatedRestaurant.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return updatedRestaurant;
        }
        catch (SqlException sqlEx)
        {
            throw new Exception(sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
