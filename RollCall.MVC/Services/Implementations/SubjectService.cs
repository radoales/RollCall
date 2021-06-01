namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using RollCall.MVC.ViewModels.SchoolClass;
    using RollCall.MVC.ViewModels.Subjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

    public class SubjectService : ISubjectServices
    {
        private readonly RollCallDbContext context;
        private readonly ReadOnlyDbContext readOnlyDbContext;
        private readonly UserManager<User> userManager;

        public SubjectService(RollCallDbContext context,
            UserManager<User> userManager, ReadOnlyDbContext readOnlyDbContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.readOnlyDbContext = readOnlyDbContext;
        }

        public async Task AddUserToSubject(string userId, int subjectId)
        {
            var schoolClasses = await this.context
                .SchoolClasses
                .Where(x => x.SubjectId == subjectId && x.ClassStartTime.Date >= DateTime.Today.Date)
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

        public async Task<int> Create(string name)
        {
            var subject = new Subject
            {
                Name = name
            };

            this.context.Subjects.Add(subject);
            await this.context.SaveChangesAsync();

            return subject.Id;
        }

        public async Task Delete(int id)
        {
            var subject = await this.context.Subjects.FindAsync(id);
            this.context.Subjects.Remove(subject);
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

        public async Task<IEnumerable<Subject>> GetAllSubjects(string slot)
        {
            var result = slot switch
            {
                PastSubjects => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.Classes.OrderBy(c => c.ClassStartTime.Date).Last().ClassStartTime.Date < DateTime.Now.Date)
                .OrderBy(x => x.Name)
                .ToListAsync(),

                UpcomingSubjects => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.Classes.OrderBy(c => c.ClassStartTime.Date).First().ClassStartTime.Date > DateTime.Now.Date || x.Classes.Count == 0)
                .OrderBy(x => x.Name)
                .ToListAsync(),

                _ => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.Classes.OrderBy(c => c.ClassStartTime.Date).First().ClassStartTime.Date <= DateTime.Now.Date
                         && x.Classes.OrderBy(c => c.ClassStartTime.Date).Last().ClassStartTime.Date >= DateTime.Now.Date)
                .OrderBy(x => x.Name)
                .ToListAsync()
            };

            return result;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsByUser(string userId, string slot)
        {
            var result = slot switch
            {
                PastSubjects => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.UsersSubjects.Any(x => x.UserId == userId) && x.Classes.OrderBy(c => c.ClassStartTime.Date).Last().ClassStartTime.Date < DateTime.Now.Date)
                .OrderBy(x => x.Name)
                .ToListAsync(),

                UpcomingSubjects => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.UsersSubjects.Any(x => x.UserId == userId) && x.Classes.OrderBy(c => c.ClassStartTime.Date).First().ClassStartTime.Date > DateTime.Now.Date)
                .OrderBy(x => x.Name)
                .ToListAsync(),

                _ => await this.readOnlyDbContext
                .Subjects
                .Where(x => x.UsersSubjects.Any(x => x.UserId == userId) && (x.Classes.OrderBy(c => c.ClassStartTime.Date).First().ClassStartTime.Date <= DateTime.Now.Date
                         && x.Classes.OrderBy(c => c.ClassStartTime.Date).Last().ClassStartTime.Date >= DateTime.Now.Date))
                .OrderBy(x => x.Name)
                .ToListAsync()
            };

            return result;
        }

        public async Task<DetailsSubjectVM> GetIndexSubjectVM(int id)
        {
            var allTeachers = await this.userManager.GetUsersInRoleAsync(Roles.TeacherRole);

            var result = await this.context
                .Subjects
                .Select(x => new DetailsSubjectVM
                {
                    Subject = x,
                    SchoolClasses = x.Classes.OrderBy(c => c.ClassStartTime).ToList(),
                    Students = x.UsersSubjects
                                .Where(y => y.SubjectId == id && !allTeachers.Contains(y.User))
                                .Select(y => y.User)
                })
                .FirstOrDefaultAsync(x => x.Subject.Id == id);

            result.Teachers = await this.context.
                 UsersSubjects
                 .Include(x => x.User)
                 .Where(x => allTeachers.Contains(x.User) && x.SubjectId == result.Subject.Id)
                 .Select(x => x.User)
                 .ToListAsync();

            return result;
        }

        public SelectList GetSubjectsAsSelectedList()
        {
            var subjects = this.context
                .Subjects
                .OrderBy(x => x.Name)
                .ToList();

            subjects.Insert(0, new Subject { Id = 0, Name = "Select Subject" });

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

        public SelectList GetUsersSubjectsAsSelectedList(string loggedInUserId, string userForDetailId)
        {
            var subjects = this.context
                 .Subjects
                 .Where(x => x.UsersSubjects.Any(us => us.UserId == loggedInUserId) && x.UsersSubjects.Any(us => us.UserId == userForDetailId))
                 .ToList();

            subjects.Insert(0, new Subject { Id = 0, Name = "Select Subject" });

            return new SelectList(subjects, "Id", "Name");
        }

        public async Task<bool> HasClassessOrUsers(int id)
        {
            return await this.context
                .UsersSubjects
                .AnyAsync(x => x.SubjectId == id);
        }

        public async Task<bool> IsSubjectExisting(string name)
        {
            return await this.readOnlyDbContext
                .Subjects
                .AnyAsync(x => x.Name == name);
        }

        public async Task RemoveUserFromSubject(string userId, int subjectId)
        {
            var userSubject = await this.context
                .UsersSubjects
                .FindAsync(userId, subjectId);

            var attendances = await this.context
                .Attendances
                .Where(x => x.Class.SubjectId == subjectId && x.UserId == userId)
                .ToListAsync();


            this.context.RemoveRange(attendances);

            this.context.Remove(userSubject);

            await this.context.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            var subject = await this.context.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            subject.Name = name;

            this.context.Subjects.Update(subject);
            await this.context.SaveChangesAsync();
        }

        public async Task<double> GetAverageSubjectAttendance(int id)
        {
            var attendances = await this.context
                .Attendances
                .Where(x => x.Class.SubjectId == id && x.Class.ClassEndTime < DateTime.Now)
                .Select(x => new ListAtendanceVM
                {
                    CheckIn_Start = x.CheckIn_Start,
                    CheckIn_Middle = x.CheckIn_Middle,
                    CheckIn_End = x.CheckIn_End
                })
                .ToListAsync();

            return attendances.Count > 0 ? Math.Ceiling(attendances.Average(x => x.AttendancePersentage)) : 0;
        }

        public async Task<int> CreateMany(List<CreateSubjectListVM> subjects)
        {
            var subjectsFromDb = await this.readOnlyDbContext
                .Subjects
                .ToListAsync();
            var subjectsToAdd = new List<Subject>();
            var counter = 0;
            foreach (var subject in subjects)
            {
                if (!subjectsFromDb.Any(x => x.Name == subject.Name))
                {
                    var subjectToAdd = new Subject
                    {
                        Name = subject.Name
                    };
                    subjectsToAdd.Add(subjectToAdd);
                    counter++;
                }

            }

            await this.context.Subjects.AddRangeAsync(subjectsToAdd);
            await this.context.SaveChangesAsync();

            return counter;
        }
    }
}
