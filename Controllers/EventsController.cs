using CollegeEventManagementSystem.Data;
using CollegeEventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeEventManagementSystem.Controllers;

public class EventsController : Controller
{
    private readonly ApplicationDbContext _db;
    public EventsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? search)
    {
        var query = _db.Events.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.Title.Contains(search) || e.Category.Contains(search) || e.Venue.Contains(search));
        ViewBag.Search = search;
        return View(await query.OrderBy(e => e.EventDate).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
        return item == null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new Event());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event item)
    {
        if (!ModelState.IsValid) return View(item);
        _db.Events.Add(item);
        await _db.SaveChangesAsync();
        TempData["Message"] = "Event created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Events.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Event item)
    {
        if (id != item.Id) return BadRequest();
        if (!ModelState.IsValid) return View(item);
        _db.Events.Update(item);
        await _db.SaveChangesAsync();
        TempData["Message"] = "Event updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.Events.FindAsync(id);
        if (item != null)
        {
            var registrations = _db.EventRegistrations.Where(r => r.EventId == id);
            _db.EventRegistrations.RemoveRange(registrations);
            _db.Events.Remove(item);
            await _db.SaveChangesAsync();
        }
        TempData["Message"] = "Event deleted.";
        return RedirectToAction(nameof(Index));
    }
}
