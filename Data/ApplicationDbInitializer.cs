﻿using OperationCHAN.Models;
using Microsoft.AspNetCore.Identity;

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
            
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();

            db.SaveChanges();
        }
    }
}