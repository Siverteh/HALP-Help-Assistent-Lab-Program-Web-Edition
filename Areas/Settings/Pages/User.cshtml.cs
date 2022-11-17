using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace OperationCHAN.Areas.Settings.Pages;

public class User : PageModel
{
    private readonly ApplicationDbContext _db;
    public User(ApplicationDbContext db)
    {
        _db = db;
    }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IActionResult OnGet()
    {
        
        Courses = _db.Courses.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now).ToList();
        return Page();
    }
    
    public IActionResult OnPostAsync()
    {
        Console.WriteLine("WORKS");
        return RedirectToPage("Index");
    }
}