using OperationCHAN.Models;
using Microsoft.AspNetCore.Identity;
using OperationCHAN.Data;

namespace OperationCHAN.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um,
            RoleManager<IdentityRole> rm, UserManager<Studas> umStudas)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();    
            db.SaveChanges();
            
             var IKT103 = new HelplistModel
            {
                Room = "C3036",
                Nickname = "Sondre",
                Description = "How to program?",
                Status = "Waiting"
                
            };
            db.HelpList.Add(IKT103);
            
            var FYS129 = new HelplistModel
            {
                Room = "A2036",
                Nickname = "Nikolai",
                Description = "How to know when the worlds ends?",
                Status = "Finished"
            };
            db.Add(FYS129);

            List<HelplistModel> IKThelplist = new List<HelplistModel>();
            IKThelplist.Add(IKT103);
            
            List<HelplistModel> PhysicsHelplist = new List<HelplistModel>();
            PhysicsHelplist.Add(FYS129);

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
            
            var studentAssistent = new Studas()
            {
                ApplicationUser = user,
                course = "ikt101"
            };
            umStudas.CreateAsync(studentAssistent, "Password1.").Wait();
            umStudas.AddToRoleAsync(studentAssistent, "StudentAssistant").Wait();
            
            db.SaveChanges(); 
        }
    }
}