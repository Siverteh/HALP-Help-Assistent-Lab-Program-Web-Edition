using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Lists.Pages;

public class List : PageModel
{
    private readonly ApplicationDbContext _db;

    public IEnumerable<CourseModel> ListItems { get; set; }
    public UserManager<ApplicationUser> _um;
    private RoleManager<IdentityRole> _rm;

    public List(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        _db = db;
        _um = um;
        _rm = rm;
    }
    
    /// <summary>
    /// The method run to load the page
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        // Check if user is logged in as studass or admin
        if (User.Identity.Name != null)
        {
            if (User.IsInRole("Admin") || (User.IsInRole("Studass")))
            {
                // Get all the entries in the Helplist for sending
                var entries = _db.Courses.ToList();

                // Place all entries into the global variable accessible to the cshtml
                ListItems = entries;

                return Page();
            }
        }
        return Redirect("/error/accessdenied");
    }

    public string? ReturnUrl { get; set; }
}