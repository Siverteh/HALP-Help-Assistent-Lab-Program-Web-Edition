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
    public String nickname;

    public Create(ApplicationDbContext db, IHubContext<HelplistHub> hubcontext)
    {
        _db = db;
        HubContext = hubcontext;
    }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IActionResult OnGet()
    {
        var user = _db.Users.FirstOrDefault(user => user.UserName == User.Identity.Name);
        if (user != null)
        {
            nickname = user.Nickname;
        }
        
        // burde vært en sjekk som sjekket om det finnes en cookie allerede, denne måttte også blitt slettet hvis ticketes blir resolved
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
        if (String.IsNullOrEmpty(Ticket.Course))
        {
            return Redirect("/error");
        }

        Ticket.Status = "Waiting";

        var t = await _db.HelpList.AddAsync(Ticket);
        await _db.SaveChangesAsync();
        
        var b = t.Entity.Id.ToString();
        
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(2),
            IsEssential = true,
            Secure = true
        };
        HttpContext.Response.Cookies.Append("MyTicket", b, cookieOptions);

        await HubContext.Clients.Groups(t.Entity.Course).SendAsync("AddToHelplist", t.Entity.Id, t.Entity.Nickname, t.Entity.Description, t.Entity.Room);
        return Redirect($"~/ticket/queue");
        
    }

}