using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OperationCHAN.Areas.Lists.Pages;
using OperationCHAN.Hubs;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    private readonly IHubContext<HelplistHub> HubContext;
    public Edit(ApplicationDbContext db, IHubContext<HelplistHub> hubcontext)
    {
        _db = db;
        HubContext = hubcontext;
    }
    
    [BindProperty] public HelplistModel Helplist { get; set; }
    
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
            return Redirect("/error/error");
        }
        Helplist = _db.HelpList.First(c => c.Id == id);
        
        return Page();
    }
    
    public async Task<IActionResult> OnPatchAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Console.WriteLine(Helplist.Id);
        
        var ticket = _db.HelpList.First(t => t.Id == Helplist.Id);
       
        ticket.Room = Helplist.Room;
        ticket.Nickname = Helplist.Nickname;
        ticket.Description = Helplist.Description;
            
        var t = _db.HelpList.Update(ticket);
        _db.SaveChanges();

        await HubContext.Clients.Groups(t.Entity.Course).SendAsync("UpdateHelplist", t.Entity.Id, t.Entity.Nickname, t.Entity.Description, t.Entity.Room);
        return Redirect($"~/ticket/queue");
        
    }

}