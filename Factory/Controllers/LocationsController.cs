using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class LocationsController : Controller
  {
    private readonly FactoryContext _db;

    public LocationsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Location> locations = _db.Locations
                            .Include(location => location.Engineers)
                            .Include(location => location.Machines)
                            .ToList();
      return View(locations);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Location location)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      else
      {
        _db.Locations.Add(location);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Location thisLocation = _db.Locations
                            .Include(location => location.Engineers)
                            .Include(location => location.Machines)
                            .FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    public ActionResult Edit(int id)
    {
            Location thisLocation = _db.Locations
                            .Include(location => location.Engineers)
                            .Include(location => location.Machines)
                            .FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    [HttpPost]
    public ActionResult Edit(Location location)
    {
      _db.Locations.Update(location);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = location.LocationId});
    }

    public ActionResult Delete(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      _db.Locations.Remove(thisLocation);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}