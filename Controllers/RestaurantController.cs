using System;
using ASPCoreHOL.Models;
using ASPCoreHOL.Services;
using ASPCoreHOL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreHOL.Controllers;

public class RestaurantController : Controller
{
    private readonly IRestaurantData _restaurantData;
    public RestaurantController(IRestaurantData restaurantData)
    {
        _restaurantData = restaurantData;
    }

    public IActionResult Index()
    {
        string username = "John Doe";
        ViewData["username"] = username;

        string[] arrName = new string[] { "Will", "John", "Tom" };
        ViewBag.arrName = arrName;

        ViewBag.address = "Jl. Kaliurang KM 5, Sleman, Yogyakarta";
        RestaurantViewModel restaurantViewModel = new RestaurantViewModel();
        restaurantViewModel.Restaurants = _restaurantData.GetAll();
        restaurantViewModel.CurrentMessage = "Hello from Restaurant Controller";

        return View(restaurantViewModel);
    }

    public IActionResult Details(int id)
    {
        var model = _restaurantData.Get(id);
        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Restaurant restaurant)
    {
        _restaurantData.Add(restaurant);
        //return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        return RedirectToAction(nameof(Index));
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
