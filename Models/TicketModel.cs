using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OperationCHAN.Models;

public class TicketModel
{
    public TicketModel() {}
        
    public TicketModel(string name, string description, string room, DateTime dateTime)
    {
        Name = name;
        Description = description;
        Room = room;
        DateTime = dateTime;
    }
    
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Text)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Text)]
    public string Room { get; set; } = string.Empty;

    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DateTime { get; set; }
}