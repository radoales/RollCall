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
        public async Task<IEnumerable<UserIndexVM>> GetAllUserIndexVMs()
        {
            return await this.context
                .Users
                .Select(x => new UserIndexVM
                {
                    Name = x.FirstName + ' ' + x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    StudentNumber = x.StudentNumber, 
                    Attendances = x.Attendances,
                     UsersSubjects = x.UsersSubjects
                }).ToListAsync();
        }
    }
}
