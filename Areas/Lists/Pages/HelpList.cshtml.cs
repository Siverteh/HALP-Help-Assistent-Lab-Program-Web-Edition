using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Lists.Pages;

public class HelpList : PageModel
{
    private readonly ApplicationDbContext _db;

    public IEnumerable<HelplistModel> Tickets { get; set; }
    public IEnumerable<string> CourseCodes { get; set; }

    public HelpList(ApplicationDbContext db)
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
        if (!CourseCodes.Contains(id))
        {
            return Redirect("/error");
        }

        // Get all the entries in the Helplist for sending
        var tickets = _db.HelpList.Where(ticket => ticket.Status == "Waiting" && ticket.Course == id);

        // Place all entries into the global variable accessible to the cshtml
        Tickets = tickets;

        return Page();
    }

    public string? ReturnUrl { get; set; }
}