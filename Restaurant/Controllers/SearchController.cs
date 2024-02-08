using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers;

public class SearchController : Controller
{
    private readonly RestaurantContext _db;
    public SearchController(RestaurantContext db)
    {
        _db = db;
    }

    private List<Cuisine> Menu()
    {
        List<Cuisine> menuData = _db.Cuisines.ToList();
        return menuData;
    }

    public async Task<ActionResult> Index(string searchType, string searchTerm)
    {
        ViewBag.Cuis = Menu();
        switch (searchType)
        {
            case "diners":
                List<Diner> dinerResults = await _db.Diners
                    .Where(diner => diner.Name.Contains(searchTerm))
                    .ToListAsync();
                return View(dinerResults);
            default:
                List<Cuisine> cuisineResults = await _db.Cuisines
                    .Where(c => c.Name.Contains(searchTerm))
                    .ToListAsync();
                return View(cuisineResults);
        }
    }
}