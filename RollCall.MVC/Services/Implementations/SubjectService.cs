namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class SubjectService : ISubjectServices
    {
        private readonly RollCallDbContext context;

        public SubjectService(RollCallDbContext context)
        {
            this.context = context;
        }

        public SelectList GetSubjectsAsSelectedList()
        {
            var subjects = this.context
                .Subjects.ToList();

            subjects.Insert(0, new Subject { Id = 0, Name = "Select Category" });

            return new SelectList(subjects, "Id", "Name");
        }

        public async Task<IEnumerable<User>> GetUsersInSubject(int id)
        {
            return await this.context
                .UsersSubjects
                .Include(x => x.Subject)
                .Where(x => x.SubjectId == id)
                .Select(x => x.User)
                .ToListAsync();
        }
    }
}
