using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new TicketModel());
    }
    [HttpPost]
    public IActionResult Index(TicketModel ticket)
    {
        if (!ModelState.IsValid)
            return View(ticket);

        var date = new DateTime().ToString("dd/MM/yyyy hh:mm");
        ticket.DateTime = Convert.ToDateTime(date);
        _db.Tickets.Add(ticket);
        _db.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}