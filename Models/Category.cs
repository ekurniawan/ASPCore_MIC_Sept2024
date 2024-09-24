using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreHOL.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}
