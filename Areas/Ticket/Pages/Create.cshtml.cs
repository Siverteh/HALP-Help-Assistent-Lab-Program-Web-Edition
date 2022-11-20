using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Create : PageModel
{
    private readonly ApplicationDbContext _db;
    public Create(ApplicationDbContext db)
    {
        _db = db;
    }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IActionResult OnGet()
    {

        Courses = _db.Courses.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now).ToList();
        return Page();
    }

}