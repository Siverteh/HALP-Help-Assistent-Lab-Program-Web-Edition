using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Areas.Lists.Pages;

public class HelpList : PageModel
{
    private readonly ApplicationDbContext _db;

    public IEnumerable<HelplistModel> ListItems { get; set; }

    public HelpList(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var entry = _db.HelpList.ToList();

        ListItems = entry;

        return Page();
    }

    public string? ReturnUrl { get; set; }
}