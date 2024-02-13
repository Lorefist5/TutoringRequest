using Microsoft.EntityFrameworkCore;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data;

public class TutoringDbContext : DbContext
{
    public TutoringDbContext(DbContextOptions<TutoringDbContext> options)
     : base(options)
    {
        Database.Migrate();
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; } 
    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
    public DbSet<Major> Majors { get; set; }
    public DbSet<TutoringSection> TutoringSections { get; set; }
    public DbSet<ResetToken> ResetTokens { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=Test.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        List<Role> roles = new List<Role>()
        {
            new Role()
            {
                Id = Guid.NewGuid(),
                RoleName = DefaultRoles.Admin.ToString()
            },
            new Role()
            {
                Id = Guid.NewGuid() ,
                RoleName = DefaultRoles.Student.ToString()
            },
            new Role()
            {
                Id = Guid.NewGuid() ,
                RoleName = DefaultRoles.Tutor.ToString()
            }
        }; 
        modelBuilder.Entity<Role>().HasData(roles);
    }
}
