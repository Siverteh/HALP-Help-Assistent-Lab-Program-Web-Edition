using Microsoft.AspNetCore.Mvc.RazorPages;
using OperationCHAN.Data;
using OperationCHAN.Models;
using Microsoft.AspNetCore.Mvc;
using OperationCHAN.Areas.Lists.Pages;

namespace OperationCHAN.Areas.Ticket.Pages;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    public Edit(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IQueryable<HelplistModel> Helplist { get; set; }
    
    public IActionResult OnGet(int id)
    {

        Helplist = _db.HelpList.Where(c => c.Id == id).Distinct();
        
        return Page();
    }

}