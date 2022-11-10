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
    
    public IActionResult OnGet()
    {
        return Page();
    }
    
    [BindProperty]
    public HelplistModel Ticket { get; set; } = default!;
    
    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine(Ticket);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var date = new DateTime().ToString("dd/MM/yyyy hh:mm");
        Ticket.DateTime = Convert.ToDateTime(date);
        Ticket.Status = "Waiting";
        
        _db.HelpList.Add(Ticket);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Create");
    }

}