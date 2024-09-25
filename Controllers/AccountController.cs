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
    public AccountController(IUser user)
    {
        _user = user;
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

}
