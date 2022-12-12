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
            db.Add(IKT103);
            
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
                EmailConfirmed = true
               // Helplist = PhysicsHelplist
            };
            
            var studAssRole = new IdentityRole("Studass");
            rm.CreateAsync(studAssRole).Wait();
            
            var studass = new ApplicationUser()
            {
                Nickname = "cool studass",
                Email = "studass@uia.no",
                UserName = "studass@uia.no",
                DiscordTag = "Studass User", 
                EmailConfirmed = true
                // Helplist = PhysicsHelplist
            };
            um.CreateAsync(user, "Password1.").Wait();
            um.CreateAsync(studass, "Password1.").Wait();
            um.AddToRoleAsync(studass, "Studass").Wait();

            db.Studas.Add(new Studas(studass, "IKT201-G"));
            db.CourseLinks.Add(new CourseLinksModel("https://cloud.timeedit.net/uia/web/tp/ri15667y6Z0655Q097QQY656Z067057Q469W95.ics"));

            InitializeTestCourses(db);

            db.SaveChanges();
        }
        private static void InitializeTestCourses(ApplicationDbContext db)
        {
            var IKT202 = new CourseModel()
            {
                CourseCode = "IKT202-G",
                CourseRoom1 = "GRM C2 036",
            };
            
            var IKT203 = new CourseModel()
            {
                CourseCode = "IKT203-G",
                CourseRoom1 = "GRM C2 036",
            };
            
            var IKT204 = new CourseModel()
            {
                CourseCode = "IKT204-G",
                CourseRoom1 = "GRM C2 036",
            };
            
            var IKT205 = new CourseModel()
            {
                CourseCode = "IKT205-G",
                CourseRoom1 = "GRM C2 036",
            };

            db.Courses.Add(IKT202);
            db.Courses.Add(IKT203);
            db.Courses.Add(IKT204);
            db.Courses.Add(IKT205);
        }
        
    }
}