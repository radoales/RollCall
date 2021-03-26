using Microsoft.EntityFrameworkCore;
using RollCall.MVC.Data.Models;
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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UsersSubjects> UsersSubjects { get; set; }
        public DbSet<UserClasses> UserClasses { get; set; }

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

            modelBuilder.Entity<UserClasses>()
       .HasKey(up => new { up.UserId, up.SchoolClassId });

            modelBuilder.Entity<UserClasses>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserClasses)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserClasses>()
                .HasOne(uc => uc.SchoolClass)
                .WithMany(c => c.UserClasses)
                .HasForeignKey(uc => uc.SchoolClassId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RollCall.MVC.Data.Models.Attendance> Attendance { get; set; }
    }
}
