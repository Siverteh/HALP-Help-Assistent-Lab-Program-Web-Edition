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

    public async Task SetStudass(string userName, string courseCode, bool setStudass)
    {
        // TODO this method will create errors later, as roles have to be updated in the database
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        
        if (setStudass)
        {
            if (user.Role == "admin")
            {
                return;
            }
            
            _db.Studas.Add(new Studas(user, courseCode));
            user.Role = "studass";
        }
        else
        {
            Studas studas = _db.Studas.First(studas => studas.ApplicationUserId == user.Id 
                                                       && studas.Course == courseCode);
            
            _db.Studas.Remove(studas);
            var otherCourses = _db.Studas.Where(s => s.ApplicationUserId == user.Id
                                                && s.Course != courseCode).ToList();
            if (otherCourses.Count <= 0)
            {
                user.Role = "user";
            }
        }
        await _db.SaveChangesAsync();
    }
    
    public async Task SetAdmin(string userName, bool setAdmin)
    {
        ApplicationUser user = _db.Users.First(user => user.Nickname == userName);
        if (setAdmin)
        {
            if (user.Role == "studass")
            {
                var courses = _db.Studas.Where(s => s.ApplicationUserId == user.Id).ToList();
                foreach (var course in courses)
                {
                    _db.Studas.Remove(course);
                }
            }
            
            await _um.AddToRoleAsync(user, "Admin");
            user.Role = "admin";
        }
        else
        {
            await _um.RemoveFromRoleAsync(user, "Admin");
            user.Role = "user";
        }
        await _db.SaveChangesAsync();
        await GetUserData(userName);
    }
    
    public async Task GetUserData(string userName)
    {
        var user = _db.Users.First(user => user.Nickname == userName);
        bool isAdmin = user.Role.Equals("admin");
        var courses = _db.Studas.Where(studass => studass.ApplicationUserId == user.Id)
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