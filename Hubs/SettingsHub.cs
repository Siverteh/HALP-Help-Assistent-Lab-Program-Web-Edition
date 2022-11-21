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

    public async Task SetStudass(int studentID, string courseCode)
    {
        ApplicationUser user = _db.Users.First(user => user.Id == studentID);
        _db.Studas.Add(new Studas(user, courseCode));
        await _db.SaveChangesAsync();
        //await Clients.All.SendAsync("Success", ticketID, nickname, description);
    }
    
    public async Task SetAdmin(int studentID)
    {
        ApplicationUser user = _db.Users.First(user => user.Id == studentID);
        _um.AddToRoleAsync(user, "Admin").Wait();
        await _db.SaveChangesAsync();
        //await Clients.All.SendAsync("AddToHelplist", ticketID, nickname, description);
    }
    
    public async Task GetStudent(int studentID)
    {
        bool isAdmin = _db.Users.Where(user => user.Id == studentID)
            .Select(user => user.Role == "Admin").First();
        var courses = _db.Studas.Where(studass => studass.Id == studentID)
            .Select(studass => studass.Course).ToList();
        await Clients.Caller.SendAsync("ShowStudent", courses, isAdmin);
    }
}