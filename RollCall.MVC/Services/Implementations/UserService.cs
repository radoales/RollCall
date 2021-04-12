namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using RollCall.MVC.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

    public class UserService : IUserService
    {

        private readonly RollCallDbContext context;
        private readonly ISubjectServices subjectServices;
        private readonly UserManager<User> userManager;

        public UserService(
            RollCallDbContext context, 
            ISubjectServices subjectServices,
            UserManager<User> userManager)
        {
            this.context = context;
            this.subjectServices = subjectServices;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserIndexVM>> GetAllTeachersStudentsAsIndexVM(string teacherId)
        {
            var teachersSubjects = await this.context
                .Subjects
                .Where(x => x.UsersSubjects.Any(us => us.UserId == teacherId))
                .Select(x => x.Id)
                .ToListAsync();

            return await this.context
                .Users
                .Where(x => x.UsersSubjects.Any(us => teachersSubjects.Contains(us.SubjectId)))
                .Select(x => new UserIndexVM
                {
                    Id = x.Id,
                    Name = x.FirstName + ' ' + x.LastName,
                    StudentNumber = x.StudentNumber,
                    Attendances = x.Attendances,
                    UsersSubjects = x.UsersSubjects
                }).ToListAsync();
        }

        public async Task<IEnumerable<UserIndexVM>> GetAllUsersAsIndexVM()
        {
            return await this.context
                .Users
                .Select(x => new UserIndexVM
                {
                    Id = x.Id,
                    Name = x.FirstName + ' ' + x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    StudentNumber = x.StudentNumber,
                    Attendances = x.Attendances,
                    UsersSubjects = x.UsersSubjects
                }).ToListAsync();
        }

        public async Task<UserDetailVM> GetAsUserDetailVM(string id)
        {
            var allTeachers = await this.userManager.GetUsersInRoleAsync(Roles.TeacherRole);

            return await this.context
                .Users
                .Select(x => new UserDetailVM
                {
                    Id = x.Id,
                    Attendances = x.Attendances.Select(a => new ListAtendanceVM
                    {
                        Id = a.Id,
                        CheckIn_Start = a.CheckIn_Start,
                        CheckIn_Middle = a.CheckIn_Middle,
                        CheckIn_End = a.CheckIn_End,
                        ClassId = a.ClassId,
                        UserId = a.UserId,
                        User = a.User,
                        Class = a.Class
                    }).Where(x => !allTeachers.Contains(x.User)).ToList(),
                    Name = x.FirstName + ' ' + x.LastName,
                    StudentNumber = x.StudentNumber, 
                    UsersSubjects = x.UsersSubjects                    
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserDetailVM> GetAsUserDetailVM(string id, int subjectId)
        {
            var allTeachers = await this.userManager.GetUsersInRoleAsync(Roles.TeacherRole);

            return await this.context
                .Users
                .Select(x => new UserDetailVM
                {
                    Id = x.Id,
                    Attendances = x.Attendances.Select(a => new ListAtendanceVM
                    {
                        Id = a.Id,
                        CheckIn_Start = a.CheckIn_Start,
                        CheckIn_Middle = a.CheckIn_Middle,
                        CheckIn_End = a.CheckIn_End,
                        ClassId = a.ClassId,
                        UserId = a.UserId,
                        User = a.User,
                        Class = a.Class
                    }).Where(x => x.Class.SubjectId == subjectId && !allTeachers.Contains(x.User))
                    .OrderByDescending(a => a.Class.ClassStartTime).ToList(),
                    Name = x.FirstName + ' ' + x.LastName,
                    StudentNumber = x.StudentNumber,
                    UsersSubjects = x.UsersSubjects
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
