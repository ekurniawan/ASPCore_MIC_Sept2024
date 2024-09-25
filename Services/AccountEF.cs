using System;
using ASPCoreHOL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ASPCoreHOL.Services;

public class AccountEF : IAccountData
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountEF(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task AddRole(string rolename)
    {
        try
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(rolename);
            if (!isRoleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(rolename));
                if (!result.Succeeded)
                    throw new Exception("Failed to create role");
            }
            else
            {
                throw new Exception("Role already exist");
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task AssignRole(string username, string rolename)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = await _userManager.AddToRoleAsync(user, rolename);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
            await AssignRole(model.Username, "admin");
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
