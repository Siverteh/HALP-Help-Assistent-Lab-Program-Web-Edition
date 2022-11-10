using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OperationCHAN.Models;

public class HelplistModel
{
    public HelplistModel(){}
    
    public HelplistModel(string nickname, string description, string room, DateTime dateTime)
    {
        Nickname = nickname;
        Description = description;
        Room = room;
        DateTime = dateTime;
    }


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

    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DateTime { get; set; }

}