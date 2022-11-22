using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Lists.Pages;

public class List : PageModel
{
    private readonly ApplicationDbContext _db;

    public IEnumerable<CourseModel> ListItems { get; set; }

    public List(ApplicationDbContext db)
    {
        _db = db;
    }
    
    /// <summary>
    /// The method run to load the page
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        // Get all the entries in the Helplist for sending
        var entries = _db.Courses.ToList();

        // Place all entries into the global variable accessible to the cshtml
        ListItems = entries;

        return Page();
    }

    public string? ReturnUrl { get; set; }
}