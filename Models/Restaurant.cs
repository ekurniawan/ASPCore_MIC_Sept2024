using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.Models;

public class Restaurant
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the restaurant name")]
    public string? Name { get; set; }
}
