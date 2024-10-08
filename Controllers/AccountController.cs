using System;
using System.Net;
using System.Security.Claims;
using ASPCoreHOL.Migrations;
using ASPCoreHOL.Models;
using ASPCoreHOL.Services;
using ASPCoreHOL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreHOL.Controllers;

public class AccountController : Controller
{
    private readonly IUser _user;
    private readonly IAccountData _accountData;

    public AccountController(IUser user, IAccountData accountData)
    {
        _user = user;
        _accountData = accountData;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        var user = new User
        {
            Username = model.Username,
            Password = model.Password
        };

        // Login
        try
        {
            var loginUser = _user.Login(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginUser.Username),
                    new Claim(ClaimTypes.Role, loginUser.Role)
                };
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberLogin
                });

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ViewBag.Message = ex.Message;

        }

        return View(model);
    }



    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = model.RoleName
            };

            // Registration
            try
            {
                _user.Registration(user);
                ViewBag.Message = "Registration successful";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }


    //asp identity
    #region  ASP Identity

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Registration
            try
            {
                await _accountData.Register(model);
                ViewBag.Message = "Registration successful";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return View(model);
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        var result = await _accountData.Login(userViewModel);
        if (result)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(userViewModel);
        }
    }

    public IActionResult AddRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(string rolename)
    {
        try
        {
            await _accountData.AddRole(rolename);
            ViewBag.Message = "Role added successfully";
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View();
    }

    public IActionResult AsignUserToRole()
    {
        return View();
    }

    #endregion



}
