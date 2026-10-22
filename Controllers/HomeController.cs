using CollegeEventManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeEventManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.EventCount = await _db.Events.CountAsync();
        ViewBag.RegistrationCount = await _db.EventRegistrations.CountAsync();
        return View(await _db.Events.OrderBy(e => e.EventDate).ToListAsync());
    }
}
