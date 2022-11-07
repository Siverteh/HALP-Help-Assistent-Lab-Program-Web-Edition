using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class StudentUser : IdentityUser
{
    public string? DiscordTag { get; set; } = String.Empty;
    
    public bool IsStudass { get; set; } = false;
    
    public string Name{ get; set; } = String.Empty;
    
}
