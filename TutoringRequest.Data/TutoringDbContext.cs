using Microsoft.EntityFrameworkCore;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data;

public class TutoringDbContext : DbContext
{
    public TutoringDbContext(DbContextOptions<TutoringDbContext> options)
     : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
    public DbSet<Major> Majors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<TutoringSection> TutoringSections { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<AdminAccountInfo> AdminAccountInfos { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=Test.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAccountInfo>()
            .HasOne(ai => ai.Administrator)
            .WithOne(a => a.AdminAccountInfo)
            .HasForeignKey<AdminAccountInfo>(ai => ai.AdminId);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.StudentNumber)
            .IsUnique();
    }
}
