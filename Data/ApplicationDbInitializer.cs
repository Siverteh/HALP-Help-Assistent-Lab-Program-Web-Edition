using OperationCHAN.Models;
using Microsoft.AspNetCore.Identity;
using OperationCHAN.Data;

namespace OperationCHAN.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<StudentUser> um,
            RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();    
            db.SaveChanges();
            
            var admin = new StudentUser
                { UserName = "admin@uia.no", Email = "admin@uia.no", DiscordTag = "Admin User", EmailConfirmed = true };

            var user = new StudentUser
                { UserName = "user@uia.no", Email = "user@uia.no", DiscordTag = "User User", EmailConfirmed = true };
            
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();
            um.CreateAsync(user, "Password1.").Wait();

            db.SaveChanges(); 
        }
    }
}