using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OperationCHAN.Areas.List;

public class HelpListModel : PageModel
{
    public void OnGet()
    {
        Console.WriteLine("Yay");
    }
    
    public HelpListModel(){}
    
    public string ReturnUrl { get; set; }
}