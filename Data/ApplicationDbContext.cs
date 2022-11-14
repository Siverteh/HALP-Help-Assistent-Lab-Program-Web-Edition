using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Models;

namespace OperationCHAN.Data;

public class ApplicationDbContext : IdentityDbContext<StudentUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CourseModel> Courses => Set<CourseModel>();

    public DbSet<HelplistModel> HelpList => Set<HelplistModel>();

}