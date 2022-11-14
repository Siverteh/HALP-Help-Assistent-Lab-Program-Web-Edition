using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class Studas
{
   public Studas () {}
    
    public int Id { get; set; }
    
    public string course { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; }
    
    public string ApplicationUserId { get; set; }
}