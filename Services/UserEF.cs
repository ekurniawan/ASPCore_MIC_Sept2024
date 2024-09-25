using System;
using ASPCoreHOL.Data;
using ASPCoreHOL.Models;
using BC = BCrypt.Net.BCrypt;

namespace ASPCoreHOL.Services;

public class UserEF : IUser
{
    private readonly ApplicationDbContext _db;
    public UserEF(ApplicationDbContext db)
    {
        _db = db;
    }

    public User Login(User user)
    {
        var _user = _db.Users.FirstOrDefault(u => u.Username == user.Username);
        if (_user == null)
        {
            throw new Exception("User not found");
        }
        if (!BC.Verify(user.Password, _user.Password))
        {
            throw new Exception("Invalid password");
        }
        return _user;
    }

    public User Registration(User user)
    {
        try
        {
            user.Password = BC.HashPassword(user.Password);
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
