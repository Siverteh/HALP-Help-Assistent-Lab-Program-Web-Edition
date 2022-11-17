using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class Studas
{
   public Studas () {}

   public Studas(ApplicationUser ap, string course)
   {
       ApplicationUser = ap;
       Course = course;
   }
    
    public int Id { get; set; }
    
    public string Course { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;
    
    public string ApplicationUserId { get; set; } = String.Empty;

}