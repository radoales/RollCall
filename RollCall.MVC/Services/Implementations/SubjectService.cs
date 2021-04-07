namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Subjects;
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

        public async Task AddUserToSubject(string userId, int subjectId)
        {
            var schoolClasses = await this.context
                .SchoolClasses
                .Where(x => x.SubjectId == subjectId && x.ClassStartTime.Date > DateTime.Today.Date)
                .ToListAsync();

            var attendances = new List<Attendance>();

            if (schoolClasses.Count != 0)
            {
                foreach (var sc in schoolClasses)
                {
                    var attendance = new Attendance
                    {
                        ClassId = sc.Id,
                        UserId = userId
                    };

                    attendances.Add(attendance);
                }
            }
          

            var userSubject = new UsersSubjects
            {
                UserId = userId,
                SubjectId = subjectId
            };

            this.context.Add(userSubject);
            this.context.AddRange(attendances);
            await this.context.SaveChangesAsync();
        }

        public async Task<Subject> Get(int id)
        {
            return await this.context
                .Subjects
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AddUsersToSubjectVM>> GetAddUsersToSubjectVM(string name, int subjectId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await this.context
                .Users
                .Include(x => x.UsersSubjects)
                .Where(x => !x.UsersSubjects.Any(us => us.SubjectId == subjectId))
                .Select(x => new AddUsersToSubjectVM
                {
                    User = x
                })
                .ToListAsync();
            }
            else
            {
                return await this.context
              .Users
              .Include(x => x.UsersSubjects)
              .Where(x => (x.FirstName.StartsWith(name) || x.LastName.StartsWith(name)) && !x.UsersSubjects.Any(us => us.SubjectId == subjectId))
              .Select(x => new AddUsersToSubjectVM
              {
                  User = x,
                  IsInSubject = x.UsersSubjects.Where(a => a.UserId == x.Id && a.SubjectId == subjectId).Any()
              })
              .ToListAsync();
            }

        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsByUser(string userId)
        {
            return await this.context
                .Subjects
                .Where(x => x.UsersSubjects.Any(x => x.UserId == userId))
                .ToListAsync();
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

        public async Task<IEnumerable<User>> GetUsersNotInSubject(int id)
        {
            return await this.context
                .UsersSubjects
                .Include(x => x.Subject)
                .Where(x => x.SubjectId == id)
                .Select(x => x.User)
                .ToListAsync();
        }

        public async Task RemoveUserFromSubject(string userId, int subjectId)
        {
            var userSubject = await this.context
                .UsersSubjects
                .FindAsync(userId, subjectId);

            var attendances = await this.context
                .Attendance
                .Where(x => x.Class.SubjectId == subjectId && x.UserId == userId)
                .ToListAsync();


                this.context.RemoveRange(attendances);

            this.context.Remove(userSubject);
          
            await this.context.SaveChangesAsync();
        }
    }
}
