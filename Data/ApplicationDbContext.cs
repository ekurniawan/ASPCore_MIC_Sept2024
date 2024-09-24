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
}

