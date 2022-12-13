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
    
    public IActionResult OnGet()
    {
        Links = _db.CourseLinks.ToList();
        Users = _db.Users.ToList();
        Courses = _db.Courses.Select(course => course.CourseCode).Distinct().ToList();
        Studasses = _db.Studas.ToList();
        var loggedInUser = _um.GetUserAsync(User).Result;
        if (loggedInUser != null)
        {
            var roleObject = _db.UserRoles.FirstOrDefault(userRole => userRole.UserId == loggedInUser.Id);
            if (roleObject != null)
            {
                var roleID = roleObject.RoleId;
                var role = _db.Roles.First(role => role.Id == roleID).Name;
                Role = role;
            }
            else
            {
                Role = "User";
            }
        }
        else
        {
            return Redirect("~/error/accessdenied");
        }

        return Page();
    }
    
    [BindProperty] public string? Link { get;set; }

    public async Task<IActionResult> OnPostAdd(string test)
    {
        Links = _db.CourseLinks.ToList();
        Users = _db.Users.ToList();
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
    }

    public async Task<IActionResult> OnPostRemove()
    {
        Links = _db.CourseLinks.ToList();
        
        Link = Link.Replace("html", "ics");
        
        var links = _db.CourseLinks.Where(l => l.CourseLink == Link).ToList();
        if (links.Count > 0)
        {
            _db.CourseLinks.Remove(links.First());
            await _db.SaveChangesAsync();
        }
        
        return Redirect("~/settings/#timeedit");
    }
}