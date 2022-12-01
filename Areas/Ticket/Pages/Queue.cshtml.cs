using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Areas.Lists.Pages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Queue : PageModel
{
    private readonly ApplicationDbContext _db;
    
    public IEnumerable<int> HelpLists { get; set; }
    public HelplistModel Ticket { get; set; }
    private IQueryable<HelplistModel> Tickets { get; set; }
    
    public int Count { get; set; }
    
    public Queue(ApplicationDbContext db)
    {
        _db = db;
        HelpLists = _db.HelpList.Select(helplist => helplist.Id).Distinct();
    }

    public IActionResult OnGet()
    {
        var id = 0;
        var cookie = Request.Cookies["MyTicket"];
        if (!String.IsNullOrEmpty(cookie))
        { 
            id = Int32.Parse(cookie);
        }
        
        if (id == 0)
        {
            return Redirect("/error");
        }

        var ticket = _db.HelpList.First(ticket => ticket.Id == id);
        var tickets = _db.HelpList.Where(t => t.Course == ticket.Course && t.Status == "Waiting");

        Ticket = ticket;
        Tickets = tickets;
        
        if (Ticket.Status is "Removed" or "Finished")
        {
            return Redirect("/error");
        }

        var count = 0;
        foreach (var t in tickets)
        {
            if (t.Id == ticket.Id)
            {
                break;
            }

            count++;
        }


        Count = count;
        
        return Page();
    }
}