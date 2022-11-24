using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OperationCHAN.Hubs;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Create : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<HelplistHub> HubContext;

    public Create(ApplicationDbContext db, IHubContext<HelplistHub> hubcontext)
    {
        _db = db;
        HubContext = hubcontext;
    }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IActionResult OnGet()
    {
        Courses = _db.Courses.ToList();//.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now).ToList();
        return Page();
    }
    
    [BindProperty] public HelplistModel Ticket { get;set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var courses = _db.Courses;//.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now);

        foreach (var c in courses)
        {
            if (Ticket.Room == c.CourseRoom1 ||
                Ticket.Room == c.CourseRoom2 ||
                Ticket.Room == c.CourseRoom3 ||
                Ticket.Room == c.CourseRoom4)
            {
                Ticket.Course = c.CourseCode;
            }
        }

        Ticket.Status = "Waiting";

        await _db.HelpList.AddAsync(Ticket);
        await _db.SaveChangesAsync();
        
        var t = _db.HelpList.First(t => 
                t.Description == Ticket.Description && 
                t.Course == Ticket.Course && 
                t.Nickname == Ticket.Nickname &&
                t.Status == "Waiting");

        var b = t.Id.ToString();
        
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(2),
            IsEssential = true,
            Secure = true
        };
        HttpContext.Response.Cookies.Append("MyTicket", b, cookieOptions);

        await HubContext.Clients.Groups(t.Course).SendAsync("AddToHelplist", t.Id, t.Nickname, t.Description, t.Room);
        return Redirect($"~/ticket/queue");
        
    }

}