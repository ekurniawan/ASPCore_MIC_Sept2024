using System;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreHOL.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return Content("Privacy");
    }

    public IActionResult Error()
    {
        return View();
    }
}
