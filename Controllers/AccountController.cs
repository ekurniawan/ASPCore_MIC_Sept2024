using System;
using ASPCoreHOL.Migrations;
using ASPCoreHOL.Models;
using ASPCoreHOL.Services;
using ASPCoreHOL.ViewModels;
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
