using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Controllers;
public class DinersController : Controller
{
    private readonly RestaurantContext _db;
    public DinersController(RestaurantContext db)
    {
        _db = db;
    }
    public ActionResult Index()
    {
        return View(_db.Diners.ToList());
    }

    public ActionResult Details(int id)
    {
        Diner diner = _db.Diners
            .Include(d => d.JoinEntities)
            .ThenInclude(join => join.Cuisine)
            .FirstOrDefault(d => d.DinerId == id);
        return View(diner);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Diner diner)
    {
        if (diner.Name == null)
        {
            return View();
        }
        if (diner.Specialty == null)
        {
            diner.Specialty = "No Specialty";
        }
        _db.Diners.Add(diner);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        Diner diner = _db.Diners.FirstOrDefault(d => d.DinerId == id);
        return View(diner);
    }

    [HttpPost]
    public ActionResult Edit(Diner diner)
    {
        _db.Diners.Update(diner);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddCuisine(int id)
    {
        Diner diner = _db.Diners.FirstOrDefault(d => d.DinerId == id);
        ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name");
        return View(diner);
    }

    [HttpPost]
    public ActionResult AddCuisine(Diner diner, int cuisineId)
    {
        CuisineDiner? joinEntity = _db.CuisineDiners.FirstOrDefault(join => (join.CuisineId == cuisineId && join.DinerId == diner.DinerId));
        if (joinEntity == null && cuisineId != 0)
        {
            _db.CuisineDiners.Add(new CuisineDiner() { CuisineId = cuisineId, DinerId = diner.DinerId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = diner.DinerId });
    }

    public ActionResult Delete(int id)
    {
        Diner diner = _db.Diners.FirstOrDefault(d => d.DinerId == id);
        return View(diner);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Diner diner = _db.Diners.FirstOrDefault(d => d.DinerId == id);
        _db.Diners.Remove(diner);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}