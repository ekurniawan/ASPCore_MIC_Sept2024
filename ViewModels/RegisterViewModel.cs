using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public string RoleName { get; set; } = null!;
}
