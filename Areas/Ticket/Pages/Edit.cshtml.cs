using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
            return Redirect("/error");
        }
        Helplist = _db.HelpList.First(c => c.Id == id);
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var cookie = Request.Cookies["MyTicket"];
        if (String.IsNullOrEmpty(cookie))
        {
            return Redirect("/error");
        }
        Helplist.Id =  Int32.Parse(cookie);
        var courses = _db.Courses;//.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now);

        foreach (var c in courses)
        {
            if (Helplist.Room == c.CourseRoom1 ||
                Helplist.Room == c.CourseRoom2 ||
                Helplist.Room == c.CourseRoom3 ||
                Helplist.Room == c.CourseRoom4)
            {
                Helplist.Course = c.CourseCode;
            }
        }

        if (String.IsNullOrEmpty(Helplist.Course))
        {
            return Redirect("/error");
        }

        Helplist.Status = "Waiting";

        var t = _db.HelpList.Update(Helplist);
        await _db.SaveChangesAsync();

        await HubContext.Clients.Groups(t.Entity.Course).SendAsync("UpdateHelplist", t.Entity.Id, t.Entity.Nickname, t.Entity.Description, t.Entity.Room);
        
        return Redirect($"~/ticket/queue");
        
    }

}