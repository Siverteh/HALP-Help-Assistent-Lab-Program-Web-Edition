using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using OperationCHAN.Data;
using OperationCHAN.Models;

namespace OperationCHAN.Hubs;

public class SettingsHub : Hub
{
    private ApplicationDbContext _db;
    private UserManager<ApplicationUser> _um;
    public SettingsHub(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }

    public async Task SetStudass(string userName, string courseCode)
    {
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        _db.Studas.Add(new Studas(user, courseCode));
        await _db.SaveChangesAsync();
        //await Clients.All.SendAsync("Success", ticketID, nickname, description);
    }
    
    public async Task SetAdmin(string userName)
    {
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        _um.AddToRoleAsync(user, "Admin").Wait();
        await _db.SaveChangesAsync();
        //await Clients.All.SendAsync("AddToHelplist", ticketID, nickname, description);
    }
    
    public async Task GetUserData(string userName)
    {
        var user = _db.Users.First(user => user.Nickname == userName);
        bool isAdmin = user.Role.Equals("admin");
        var courses = _db.Studas.Where(studass => studass.ApplicationUserId == user.StudasId)
            .Select(studass => studass.Course).ToList();
        await Clients.Caller.SendAsync("ShowStudent", courses, isAdmin);
    }
}