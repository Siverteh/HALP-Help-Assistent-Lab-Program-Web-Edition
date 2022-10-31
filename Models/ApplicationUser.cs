using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class ApplicationUser : IdentityUser
{
    public string Nickname { get; set; } = String.Empty;
    
    public int Age { get; set; }
}
