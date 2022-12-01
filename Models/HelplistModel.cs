using Microsoft.Build.Framework;
using OperationCHAN.Data;

namespace OperationCHAN.Models;

public class HelplistModel
{
    private ApplicationDbContext _db;
    
    public HelplistModel(){}

    public HelplistModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public int Id { get; set; }

    [Required]
    public string Nickname { get; set; } = String.Empty;
    
    [Required]
    public string Room { get; set; } = String.Empty;
  
    [Required]
    public string Course { get; set; } = String.Empty;

    public string Status { get; set; } = String.Empty;
    
   [Required]
    public string Description { get; set; } = String.Empty;

    //  Application property
    // Has been set to null to avoid errors in testdata creation. Remove null when using in production.
    public ApplicationUser? ApplicationUser { get; set; }
}