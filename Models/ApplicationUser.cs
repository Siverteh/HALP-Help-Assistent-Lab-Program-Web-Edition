using Microsoft.AspNetCore.Identity;

namespace OperationCHAN.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() {}

    public ApplicationUser(string nickname, string discordTag, List<HelplistModel> helplist)
    {
        Nickname = nickname;
        DiscordTag = discordTag;
        Helplist = helplist;
    }
    public string? DiscordTag { get; set; } = String.Empty;

    public string Nickname{ get; set; } = String.Empty;
    
    public List<HelplistModel> Helplist { get; set; } = new List<HelplistModel>();


}
