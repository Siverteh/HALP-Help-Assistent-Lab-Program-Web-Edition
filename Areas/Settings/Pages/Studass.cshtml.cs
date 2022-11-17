using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace OperationCHAN.Areas.Settings.Pages;

public class Studass : PageModel
{
    private readonly ApplicationDbContext _db;
    public Studass(ApplicationDbContext db)
    {
        _db = db;
    }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IActionResult OnGet()
    {
        
        Courses = _db.Courses.Where(c => c.LabStart <= DateTime.Now && c.LabEnd >= DateTime.Now).ToList();
        return Page();
    }
    //
    // [BindProperty]
    // public HelplistModel HelplistModel { get; set; }
    //
    // public async Task<IActionResult> OnPostAsync()
    // {
    //     Console.WriteLine(HelplistModel);
    //     if (!ModelState.IsValid)
    //     {
    //         return Page();
    //     }
    //     var date = new DateTime().ToString("dd/MM/yyyy hh:mm");
    //     HelplistModel.Created = Convert.ToDateTime(date);
    //     HelplistModel.Status = "Waiting";
    //     
    //     var entry = _db.Add(new HelplistModel());
    //     entry.CurrentValues.SetValues(HelplistModel);
    //     await _db.SaveChangesAsync();
    //     return RedirectToPage("./Create");
    //     _db.HelpList.Add(HelplistModel);
    //     await _db.SaveChangesAsync();
    //
    //     return RedirectToPage("./Create");
    //     
    // }

}