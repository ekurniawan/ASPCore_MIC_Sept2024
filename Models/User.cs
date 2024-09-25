using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.Models;

public class User
{
    [Key]
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}
