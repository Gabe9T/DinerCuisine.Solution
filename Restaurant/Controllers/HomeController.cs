using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers;

public class HomeController : Controller
{
    private readonly RestaurantContext _db;
    public HomeController(RestaurantContext db)
    {
        _db = db;
    }

    private List<Cuisine> Menu()
    {
        List<Cuisine> menuData = _db.Cuisines.ToList();
        return menuData;
    }


    [HttpGet("/")]
    public ActionResult Index()
    {
        ViewBag.Cuis = Menu();
        return View();
    }
}