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

    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }
}