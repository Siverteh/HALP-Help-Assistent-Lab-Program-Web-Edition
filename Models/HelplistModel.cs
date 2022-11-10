using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OperationCHAN.Models;

public class HelplistModel
{
    public HelplistModel(){}
    public string Status { get; set; } = String.Empty;
    
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Nickname { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Text)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Text)]
    public string Room { get; set; } = string.Empty;

    public DateTime Created { get; set; }

}