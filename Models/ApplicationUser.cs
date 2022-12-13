using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() {}

    public ApplicationUser(string nickname, string discordTag, bool discordLogin)
    {
        Nickname = nickname;
        DiscordTag = discordTag;
        DiscordLogin = discordLogin;
    }
    
    //public int Id { get; set; }
    public string DiscordTag { get; set; } = String.Empty;

    public string Nickname{ get; set; } = String.Empty;
    
    public bool DiscordLogin { get; set; } = false;
    
    public List<HelplistModel> Helplist { get; set; } = new List<HelplistModel>();

    //public Studas Studas { get; set; } = null!;

    //public string StudasId { get; set; } = String.Empty;
}
