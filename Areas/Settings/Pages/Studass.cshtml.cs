using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Settings.Pages;

public class Studass : PageModel
{
    private readonly ApplicationDbContext _db;
    public Studass(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IEnumerable<CourseLinksModel> Links { get; set; }

    public void OnGet()
    {
        Links = _db.CourseLinks.ToList();
    }
    
    [BindProperty] public string? Link { get;set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        Links = _db.CourseLinks.ToList();
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

        return Page();
    }
}