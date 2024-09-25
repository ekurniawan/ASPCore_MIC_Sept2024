using System;
using ASPCoreHOL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ASPCoreHOL.Services;

public class AccountEF : IAccountData
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountEF(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> Login(UserViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
        if (!result.Succeeded)
        {
            throw new Exception("Gagal login");
        }
        return true;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task Register(RegistrationViewModel model)
    {
        try
        {
            var newUser = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Username
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Gagal mendaftar user baru");
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
