using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;

namespace Restaurant.Controllers;

public class CuisinesController : Controller
{
    private readonly RestaurantContext _db;

    public CuisinesController(RestaurantContext db)
    {
        _db = db;
    }
    public ActionResult Index()
    {
        List<Cuisine> model = _db.Cuisines.ToList();
        return View(model);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Cuisine cuisine)
    {
        if (cuisine.Name == null)
        {
            return View(cuisine);
        }
        else
        {
            _db.Cuisines.Add(cuisine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public ActionResult Details(int id)
    {
        Cuisine thisCuisine = _db.Cuisines
            .Include(cuisine => cuisine.JoinEntities)
            .ThenInclude(join => join.Diner)
            .FirstOrDefault(cuisine => cuisine.CuisineId == id);
        return View(thisCuisine);
    }

    public ActionResult Edit(int id)
    {
        Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        return View(thisCuisine);
    }

    [HttpPost]
    public ActionResult Edit(Cuisine cuisine)
    {
        _db.Cuisines.Update(cuisine);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        return View(thisCuisine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        _db.Cuisines.Remove(thisCuisine);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddDiner(int id)
    {
        Cuisine cuisine = _db.Cuisines.FirstOrDefault(c => c.CuisineId == id);
        ViewBag.DinerId = new SelectList(_db.Diners, "DinerId", "Name");
        return View(cuisine);
    }

    [HttpPost]
    public ActionResult AddDiner(Cuisine cuisine, int dinerId)
    {
        CuisineDiner joinEntity = _db.CuisineDiners.FirstOrDefault(join => (join.DinerId == dinerId && join.CuisineId == cuisine.CuisineId));
        if (joinEntity == null && dinerId != 0)
        {
            _db.CuisineDiners.Add(new CuisineDiner() { DinerId = dinerId, CuisineId = cuisine.CuisineId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = cuisine.CuisineId });
    }
}