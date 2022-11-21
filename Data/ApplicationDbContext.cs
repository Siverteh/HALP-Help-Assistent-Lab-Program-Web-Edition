using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Models;

namespace OperationCHAN.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CourseModel> Courses => Set<CourseModel>();
    public DbSet<CourseLinksModel> CourseLinks => Set<CourseLinksModel>();

    public DbSet<HelplistModel> HelpList => Set<HelplistModel>();
    
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Studas> Studas => Set<Studas>();

}