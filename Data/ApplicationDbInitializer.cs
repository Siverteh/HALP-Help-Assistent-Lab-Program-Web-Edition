
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
            
            var admin = new ApplicationUser
                { UserName = "admin@uia.no", Email = "admin@uia.no", Nickname = "Admin User", EmailConfirmed = true };

            var user = new ApplicationUser
                { UserName = "user@uia.no", Email = "user@uia.no", Nickname = "User User", EmailConfirmed = true };
            
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();
            um.CreateAsync(user, "Password1.").Wait();

            db.SaveChanges(); 
        }
    }
}