namespace Restaurant.Models;
using System.ComponentModel.DataAnnotations;

public class Cuisine
{
    public int CuisineId { get; set; }
    public string Name { get; set; }
    public List<CuisineDiner> JoinEntities { get; }
}