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
            
             var IKT103 = new HelplistModel
            {
                Room = "C3036",
                Course = "IKT201-G",
                Nickname = "Sondre",
                Description = "How to program?",
                Status = "Waiting"
                
            };
            db.HelpList.Add(IKT103);
            
            var FYS129 = new HelplistModel
            {
                Room = "A2036",
                Nickname = "Nikolai",
                Course = "IKT201-G",
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
                //Helplist = IKThelplist
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
               // Helplist = PhysicsHelplist
            };
            um.CreateAsync(user, "Password1.").Wait();

            db.Studas.Add(new Studas(admin, "Ikt103"));
            db.CourseLinks.Add(new CourseLinksModel("https://cloud.timeedit.net/uia/web/tp/ri15667y6Z0655Q097QQY656Z067057Q469W95.ics"));
            
            db.SaveChanges(); 
        }
    }
}