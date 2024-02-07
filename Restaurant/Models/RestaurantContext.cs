using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models;

public class RestaurantContext : DbContext
{
    public DbSet<Cuisine> Cuisines { get; set; }
    public DbSet<Diner> Diners { get; set; }
    public DbSet<CuisineDiner> CuisineDiners { get; set; }
    public RestaurantContext(DbContextOptions options) : base(options) { }
}