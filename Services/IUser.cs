using System;
using ASPCoreHOL.Models;

namespace ASPCoreHOL.Services;

public interface IUser
{
    User Registration(User user);
    User Login(User user);
}
