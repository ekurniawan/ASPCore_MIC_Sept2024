using System;
using ASPCoreHOL.ViewModels;

namespace ASPCoreHOL.Services;

public interface IAccountData
{
    Task Register(RegistrationViewModel model);
    Task<bool> Login(UserViewModel model);
    Task Logout();
}
