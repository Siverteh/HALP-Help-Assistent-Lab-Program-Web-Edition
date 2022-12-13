using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using OperationCHAN.Data;
using OperationCHAN.Models;
using OperationCHAN.Hubs;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Queue : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<HelplistHub> HubContext;
    
    public HelplistModel Ticket { get; set; }
    
    public int Count { get; set; }
    
    
    public Queue(ApplicationDbContext db, IHubContext<HelplistHub> hubcontext)
    {
        _db = db;
        HubContext = hubcontext;
    }

    public async Task<IActionResult> OnGetAsync()
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
        
        Ticket = ticket;
        
        var tickets = _db.HelpList.Where(t => t.Course == ticket.Course && t.Status == "Waiting");
   
        var count = 1;
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