namespace Restaurant.Models;

public class CuisineDiner
{
    public int CuisineDinerId { get; set; }
    public int CuisineId { get; set; }
    public Cuisine Cuisine { get; set; }
    public int DinerId { get; set; }
    public Diner Diner { get; set; }
}