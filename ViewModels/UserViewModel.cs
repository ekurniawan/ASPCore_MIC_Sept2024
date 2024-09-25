using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.ViewModels;

public class UserViewModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
