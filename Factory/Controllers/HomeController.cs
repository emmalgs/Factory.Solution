using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;
    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.machines = _db.Machines
                            .Include(machine => machine.Location)
                            .ToList();
      ViewBag.engineers = _db.Engineers
                            .Include(engineer => engineer.Location)
                            .ToList();
      return View();
    }
  }
}