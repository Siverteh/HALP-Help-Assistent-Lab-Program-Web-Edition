using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Lists.Pages;

public class Archive : PageModel
{
    private readonly ApplicationDbContext _db;
    public string CourseCode { get; set; }

    public IEnumerable<HelplistModel> ListItems { get; set; }
    public IEnumerable<string> CourseCodes { get; set; }

    public Archive(ApplicationDbContext db)
    {
        _db = db;
        // Keep all course codes in RAM for more speeeed
        CourseCodes = _db.Courses.Select(course => course.CourseCode).Distinct();
    }


    /// <summary>
    /// The method run to load the page
    /// </summary>
    /// <param name="id">The course code as the last part of the URL</param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetAsync(string id)
    {
        // Check if user is logged in as studass or admin
        if (User.Identity.Name != null)
        {
            if (User.IsInRole("Admin") || (User.IsInRole("Studass")))
            {

                if (!CourseCodes.Contains(id))
                {
                    return Redirect("/error");
                }

                // Get all the entries in the Helplist for sending
                var entries = _db.HelpList.Where(ticket => ticket.Status != "Waiting" && ticket.Course == id);

                // Place all entries into the global variable accessible to the cshtml
                ListItems = entries;

                return Page();
            }
        }
        return Redirect("/error/accessdenied");
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        return Page();
    }

    public string? ReturnUrl { get; set; }
}