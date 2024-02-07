namespace Restaurant.Models;

public class Diner
{
    public int DinerId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }

    public List<CuisineDiner> JoinEntities { get; }
}