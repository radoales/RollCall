namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class UserService : IUserService
    {

        private readonly RollCallDbContext context;

        public UserService(RollCallDbContext context)
        {
            this.context = context;
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
            return await this.context
                .Users
                .Select(x => new UserDetailVM
                {
                    Id = x.Id,
                    Attendances = x.Attendances,
                    Name = x.FirstName + ' ' + x.LastName,
                    StudentNumber = x.StudentNumber, 
                    UsersSubjects = x.UsersSubjects
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
