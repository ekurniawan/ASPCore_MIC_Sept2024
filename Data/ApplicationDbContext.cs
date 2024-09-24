using System;
using ASPCoreHOL.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreHOL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
}

