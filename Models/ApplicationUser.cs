using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() {}

    public ApplicationUser(string nickname, string discordTag)
    {
        Nickname = nickname;
        DiscordTag = discordTag;
    }
    
    public int Id { get; set; }
    public string DiscordTag { get; set; } = String.Empty;

    public string Nickname{ get; set; } = String.Empty;
    
    public List<HelplistModel> Helplist { get; set; } = new List<HelplistModel>();

    public Studas Studas { get; set; } = null!;

    public string StudasId { get; set; } = String.Empty;
     
    public string Role { get; set; } = "user";
}
