using Microsoft.Build.Framework;

namespace OperationCHAN.Models;

public class HelplistModel
{
    public HelplistModel(){}

    public int Id { get; set; }

    public string Nickname { get; set; } = String.Empty;
    
    public string Room { get; set; } = String.Empty;
    
    public string Status { get; set; } = String.Empty;
    
    public string Description { get; set; } = String.Empty;

}