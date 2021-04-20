using Microsoft.EntityFrameworkCore;
using RollCall.MVC.Data.Models;
using RollCall.MVC.ViewModels.Users;
namespace RollCall.MVC.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data.Models;
    public class RollCallDbContext : IdentityDbContext<User>
    {
        public RollCallDbContext(DbContextOptions<RollCallDbContext> options)
            : base(options)
        {
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<UsersSubjects> UsersSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersSubjects>()
        .HasKey(up => new { up.UserId, up.SubjectId });

            modelBuilder.Entity<UsersSubjects>()
                .HasOne(us => us.User)
                .WithMany(u => u.UsersSubjects)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersSubjects>()
                .HasOne(us => us.Subject)
                .WithMany(s => s.UsersSubjects)
                .HasForeignKey(us => us.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
