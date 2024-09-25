using System;
using ASPCoreHOL.Models;
using ASPCoreHOL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreHOL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationData _locationData;
    public LocationController(ILocationData locationData)
    {
        _locationData = locationData;
    }

    [HttpGet]
    public IEnumerable<Location> Get()
    {
        return _locationData.GetAll();
    }

    [HttpGet("{id}")]
    public Location Get(int id)
    {
        return _locationData.Get(id);
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Create new location");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id)
    {
        return Ok($"Update location with id = {id}");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok($"Delete location with id = {id}");
    }
}
