using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.ViewModels;

public class RoleCreateViewModel
{
    [Required]
    public string? RoleName { get; set; }
}
