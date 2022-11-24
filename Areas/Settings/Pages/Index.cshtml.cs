using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Settings.Pages;

public class Settings : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    public Settings(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }

    public IEnumerable<CourseLinksModel> Links { get; set; }
    public string Role { get; set; }
    public IEnumerable<ApplicationUser> Users { get; set; }
    public IEnumerable<string> Courses { get; set; }
    
    public IEnumerable<Studas> Studasses { get; set; }


    public void OnGet()
    {
        Links = _db.CourseLinks.ToList();
        Users = _db.Users.ToList();
        Courses = _db.Courses.Select(course => course.CourseCode).Distinct().ToList();
        Studasses = _db.Studas.ToList();
        var loggedInUser = _um.GetUserAsync(User).Result;
        if (loggedInUser != null)
        {
            Role = loggedInUser.Role;
        }
    }
    
    /*[BindProperty] public string? Link { get;set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Link == null)
        {
            return Page();
        }
        if(!Link.StartsWith("https"))
        {
            return Page();
        }
        if (!Link.EndsWith(".html"))
        {
            return Page();
        }

        Link = Link.Replace("html", "ics");

        await _db.CourseLinks.AddAsync(new CourseLinksModel(Link));
        await _db.SaveChangesAsync();
        Timeedit t = new Timeedit(_db);
        await t.GetData(Link);
        return Redirect("~/settings/#timeedit");
    }*/
}