using CollegeEventManagementSystem.Data;
using CollegeEventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeEventManagementSystem.Controllers;

public class RegistrationsController : Controller
{
    private readonly ApplicationDbContext _db;
    public RegistrationsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var list = await _db.EventRegistrations.Include(r => r.Event)
            .OrderByDescending(r => r.RegisteredAt).ToListAsync();
        return View(list);
    }

    public async Task<IActionResult> Create(int eventId)
    {
        var ev = await _db.Events.FindAsync(eventId);
        if (ev == null) return NotFound();
        ViewBag.EventTitle = ev.Title;
        return View(new EventRegistration { EventId = eventId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventRegistration registration)
    {
        var ev = await _db.Events.FindAsync(registration.EventId);
        if (ev == null) return NotFound();

        if (await _db.EventRegistrations.AnyAsync(r =>
            r.EventId == registration.EventId && r.Email == registration.Email))
            ModelState.AddModelError(nameof(registration.Email), "This email is already registered for this event.");

        var count = await _db.EventRegistrations.CountAsync(r => r.EventId == registration.EventId);
        if (count >= ev.Capacity)
            ModelState.AddModelError("", "Sorry, this event has reached its registration capacity.");

        if (!ModelState.IsValid)
        {
            ViewBag.EventTitle = ev.Title;
            return View(registration);
        }

        registration.RegisteredAt = DateTime.Now;
        _db.EventRegistrations.Add(registration);
        await _db.SaveChangesAsync();
        HttpContext.Session.SetString("LastRegisteredEvent", ev.Title);
        TempData["Message"] = $"Registration successful for {ev.Title}.";
        return RedirectToAction("Index", "Home");
    }
}
