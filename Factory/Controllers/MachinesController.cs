using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> machines = _db.Machines
                                .Include(machine => machine.Location)
                                .ToList();
      return View(machines);
    }

    public ActionResult Create()
    {
      ViewBag.allLocations = _db.Locations.ToList();
      ViewBag.Status = new SelectList(Machine.StatusOptions, "Value", "Value");
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "City");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.allLocations = _db.Locations.ToList();
        ViewBag.Status = new SelectList(Machine.StatusOptions, "Value", "Value");
        ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "City");
        return View();
      }
      else
      {
        _db.Machines.Add(machine);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
                                .Include(machine => machine.Location)
                                .Include(machine => machine.JoinEntities)
                                .ThenInclude(join => join.Engineer)
                                .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.Status = new SelectList(Machine.StatusOptions, "Value", "Value");
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "City");
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }



    public ActionResult AddEngineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerId)
    {
      #nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == engineerId && join.MachineId == machine.MachineId));
      #nullable disable
      if (joinEntity == null && engineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineId = machine.MachineId, EngineerId = engineerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }
  }
}