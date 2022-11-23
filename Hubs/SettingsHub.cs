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

    public async Task SetStudass(string userName, string courseCode, bool isStudass)
    {
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        if (isStudass)
        {
            _db.Studas.Add(new Studas(user, courseCode));
        }
        else
        {
            // Remove studass
        }
        await _db.SaveChangesAsync();
    }
    
    public async Task SetAdmin(string userName, bool isAdmin)
    {
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        if (isAdmin)
        {
            _um.AddToRoleAsync(user, "Admin").Wait();
        }
        else
        {
            //Remove admin
        }
        await _db.SaveChangesAsync();
    }
    
    public async Task GetUserData(string userName)
    {
        var user = _db.Users.First(user => user.Nickname == userName);
        bool isAdmin = user.Role.Equals("admin");
        var courses = _db.Studas.Where(studass => studass.ApplicationUserId == user.StudasId)
            .Select(studass => studass.Course).ToList();
        await Clients.Caller.SendAsync("ShowStudent", courses, isAdmin);
    }

    public async Task AddTimeeditLink(string link)
    {
        _db.CourseLinks.Add(new CourseLinksModel(link));
        _db.SaveChanges();
    }

    public async Task RemoveTimeeditLink(string link)
    {
        var _link = _db.CourseLinks.First(l => l.CourseLink == link);
        if (_link != null)
        {
            _db.CourseLinks.Remove(_link);
            _db.SaveChanges();
        }
    }
}