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
        if (TempData["Message"] != null)
        {
            ViewBag.Message = TempData["Message"];
        }

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
        if (TempData["Message"] != null)
        {
            ViewBag.Message = TempData["Message"];
        }

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
        try
        {
            _restaurantData.Add(restaurant);
            TempData["Message"] = "Restaurant saved!";
            return RedirectToAction(nameof(Details), new { id = restaurant.Id });

        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"<span css='alert alert-danger'>{ex.Message}</span>";
            return View(restaurant);
        }
    }

    public IActionResult Edit(int id)
    {
        Restaurant model = _restaurantData.Get(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(Restaurant restaurant)
    {
        try
        {
            _restaurantData.Update(restaurant);
            TempData["Message"] = "Restaurant saved!";
            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"<span css='alert alert-danger'>{ex.Message}</span>";
            return View(restaurant);
        }
    }

    public IActionResult Delete(int id)
    {
        Restaurant model = _restaurantData.Get(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeletePost(Restaurant restaurant)
    {
        try
        {
            _restaurantData.Delete(restaurant.Id);
            TempData["Message"] = "Restaurant deleted!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"<span css='alert alert-danger'>{ex.Message}</span>";
            return View(restaurant);
        }
    }

    [HttpPost]
    public IActionResult SearchByName(string searchString)
    {
        try
        {
            var model = _restaurantData.GetRestaurantsByName(searchString);
            if (model == null)
            {
                TempData["Message"] = "No data found!";
                return RedirectToAction(nameof(Index));
            }

            RestaurantViewModel restaurantViewModel = new RestaurantViewModel();
            restaurantViewModel.Restaurants = model;

            return View("Index", restaurantViewModel);
        }
        catch (System.Exception ex)
        {
            ViewBag.ErrorMessage = $"<span css='alert alert-danger'>{ex.Message}</span>";
            return View("Index");
        }
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
