using OperationCHAN.Models;
using Microsoft.AspNetCore.Identity;
using OperationCHAN.Data;

namespace OperationCHAN.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um,
            RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();    
            db.SaveChanges();
            
             var IKT = new HelplistModel
            {
                Room = "C3036",
                Description = "How to program?",
            };
            db.HelpList.Add(IKT);
            
            var FYS = new HelplistModel
            {
                Room = "A2036",
                Description = "How to know when the worlds ends?",
            };
            db.Add(FYS);

            List<HelplistModel> IKThelplist = new List<HelplistModel>();
            IKThelplist.Add(IKT);
            
            List<HelplistModel> PhysicsHelplist = new List<HelplistModel>();
            PhysicsHelplist.Add(FYS);

            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            var admin = new ApplicationUser()
            {
                Nickname = "cool Admin",
                UserName = "admin@uia.no",
                Email = "admin@uia.no",
                DiscordTag = "Admin User",
                EmailConfirmed = true,
                Helplist = IKThelplist
            };
            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();
           

            var user = new ApplicationUser()
            {
                Nickname = "cool user",
                Email = "user@uia.no",
                UserName = "user@uia.no",
                DiscordTag = "User User", 
                EmailConfirmed = true,
                Helplist = PhysicsHelplist
            };
            um.CreateAsync(user, "Password1.").Wait();
            
            
            var studentassistRole = new IdentityRole("StudentAssistant");
            rm.CreateAsync(studentassistRole).Wait();
            
            var studentAssistent = new ApplicationUser()
            {
                Nickname = "cool assistent",
                UserName = "studas@uia.no",
                Email = "studas@uia.no",
                DiscordTag = "Assistant User",
                EmailConfirmed = true,
                Helplist = IKThelplist
            };
            um.CreateAsync(studentAssistent, "Password1.").Wait();
            um.AddToRoleAsync(studentAssistent, "StudentAssistant").Wait();
            
            db.SaveChanges(); 
        }
    }
}