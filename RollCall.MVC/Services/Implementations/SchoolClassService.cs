namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

    public class SchoolClassService : ISchoolClassService
    {
        private readonly RollCallDbContext context;
        private readonly ReadOnlyDbContext readOnlyDbContext;
        private readonly ISubjectServices subjectServices;
        private readonly UserManager<User> userManager;

        public SchoolClassService(RollCallDbContext context, ISubjectServices subjectServices, UserManager<User> userManager, ReadOnlyDbContext readOnlyDbContext)
        {
            this.context = context;
            this.subjectServices = subjectServices;
            this.userManager = userManager;
            this.readOnlyDbContext = readOnlyDbContext;
        }

        public async Task Create(DateTime classStartTime, DateTime classEndTime, int subjectId)
        {
            var schoolClass = new SchoolClass
            {
                SubjectId = subjectId,
                ClassStartTime = classStartTime,
                ClassEndTime = classEndTime
            };

            this.context.Add(schoolClass);
            await this.context.SaveChangesAsync();

            var usersInSubject = await this.subjectServices.GetUsersInSubject(subjectId);

            var attendances = new List<Attendance>();

            foreach (var user in usersInSubject)
            {
                var attendance = new Attendance
                {
                    ClassId = schoolClass.Id,
                    UserId = user.Id
                };

                attendances.Add(attendance);
            }

            await this.context.AddRangeAsync(attendances);
            await this.context.SaveChangesAsync();

        }

        public async Task<int> GetCurrentBlock(int classId)
        {
            var schoolClass = await this.context.SchoolClasses.FirstOrDefaultAsync(x => x.Id == classId);
            var classDuration = schoolClass.ClassEndTime - schoolClass.ClassStartTime;
            var blockDuration = classDuration / 3;

            var endBlock1 = schoolClass.ClassStartTime + blockDuration;
            var endBlock2 = endBlock1 + blockDuration;
            var endBlock3 = schoolClass.ClassEndTime;

            var currentBlock =
                  DateTime.Now < endBlock1 && DateTime.Now >= schoolClass.ClassStartTime ? 1
                : DateTime.Now > endBlock1 && DateTime.Now < endBlock2 ? 2
                : DateTime.Now > endBlock2 && DateTime.Now < endBlock3 ? 3
                : 0;

            return currentBlock;
        }

        public async Task Delete(int id)
        {
            var schoolClass = await this.context.SchoolClasses.FindAsync(id);
            this.context.SchoolClasses.Remove(schoolClass);
            await this.context.SaveChangesAsync();
        }

        public async Task GenerateCode(int id)
        {
            var schoolClass = await this.context
                .SchoolClasses
                .FirstOrDefaultAsync(x => x.Id == id);

            var random = new Random();

            schoolClass.Code = random.Next(RoomCodeMin, RoomCodeMax);
            schoolClass.CodeGeneratedTime = DateTime.Now;

            this.context.Update(schoolClass);
            await this.context.SaveChangesAsync();
        }

        public async Task<DetailsSchoolClassVM> GetDetailsSchoolClassVM(int id)
        {
            var currentBlock = await GetCurrentBlock(id);
            var allTeachers = await this.userManager.GetUsersInRoleAsync(Roles.TeacherRole);

            var result = await this.context
                .SchoolClasses
                 .Include(s => s.Subject)
                 .Include(x => x.Attendances)
                 .ThenInclude(x => x.User)
                 .Select(x => new DetailsSchoolClassVM
                 {
                     Id = x.Id,
                     Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                     CurrentBlock = currentBlock,
                     Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
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
                     }).Where(x => !allTeachers.Contains(x.User))
                     .OrderBy(x => x.User.FirstName)
                     .ToList(),
                     Code = x.Code,
                     SubjectId = x.SubjectId,
                     CodeGeneratedTime = x.CodeGeneratedTime.Value.AddMinutes(TimeToCheckIn).ToString("MMM d, yyyy HH':'mm':'ss"),
                     Subject = x.Subject,
                     TimeLeft = x.CodeGeneratedTime != null ? (DateTime)x.CodeGeneratedTime : null
                 })
                 .FirstOrDefaultAsync(x => x.Id == id);

            result.Teachers = await this.context.
                 UsersSubjects
                 .Include(x => x.User)
                 .Where(x => allTeachers.Contains(x.User) && x.SubjectId == result.SubjectId)
                 .Select(x => x.User)
                 .ToListAsync();

            return result;
        }

        public async Task<DetailsSchoolClassVM> GetAsStudent(int id, string userId)
        {
            var currentBlock = await GetCurrentBlock(id);
            var allTeachers = await this.userManager.GetUsersInRoleAsync(Roles.TeacherRole);

            var result = await this.context.SchoolClasses
                 .Include(s => s.Subject)
                 .Include(x => x.Attendances)
                 .ThenInclude(x => x.User)
                 .Select(x => new DetailsSchoolClassVM
                 {
                     Id = x.Id,
                     Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                     CurrentBlock = currentBlock,
                     Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                     Attendances = x.Attendances.Where(a => a.UserId == userId)
                     .Select(a => new ListAtendanceVM
                     {
                         Id = a.Id,
                         CheckIn_Start = a.CheckIn_Start,
                         CheckIn_Middle = a.CheckIn_Middle,
                         CheckIn_End = a.CheckIn_End,
                         ClassId = a.ClassId,
                         UserId = a.UserId,
                         User = a.User,
                         Class = a.Class
                     }).OrderBy(x => x.User.FirstName)
                     .ToList(),
                     Code = x.Code,
                     SubjectId = x.SubjectId,
                     CodeGeneratedTime = x.CodeGeneratedTime.Value.AddMinutes(30).ToString("MMM d, yyyy HH':'mm':'ss"),
                     Subject = x.Subject,
                     TimeLeft = x.CodeGeneratedTime != null ? (DateTime)x.CodeGeneratedTime : null
                 })
                 .FirstOrDefaultAsync(x => x.Id == id);

            result.Teachers = await this.context.
                UsersSubjects
                .Include(x => x.User)
                .Where(x => allTeachers.Contains(x.User) && x.SubjectId == result.SubjectId)
                .Select(x => x.User)
                .ToListAsync();

            return result;
        }

        public async Task Update(int id, DateTime startTime, DateTime endTime, int subjectId)
        {
            var schoolClass = await this.context.SchoolClasses.FirstOrDefaultAsync(x => x.Id == id);

            schoolClass.ClassStartTime = startTime;
            schoolClass.ClassEndTime = endTime;
            schoolClass.SubjectId = subjectId;

            this.context.Update(schoolClass);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexSchoolClassVM>> GetTodaysClasses()
        {
            return await this.readOnlyDbContext
                 .SchoolClasses
                 .Include(x => x.Subject)
                 .ThenInclude(x => x.UsersSubjects)
                 .Where(x => x.ClassStartTime.Date == DateTime.Now.Date)
                 .Select(x => new IndexSchoolClassVM
                 {
                     Id = x.Id,
                     ClassStartTime = x.ClassStartTime,
                     ClassEndTime = x.ClassEndTime,
                     Code = x.Code,
                     Subject = x.Subject,
                     SubjectId = x.SubjectId,
                     Attendances = x.Attendances,
                     UsersInClass = x.Attendances.Count,
                     Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                     IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
                 })
                 .ToListAsync();
        }
        public async Task<IEnumerable<IndexSchoolClassVM>> GetTodaysLoggedInUserClasses(string userId)
        {
            return await this.readOnlyDbContext
                .SchoolClasses
                .Include(x => x.Subject)
                .ThenInclude(x => x.UsersSubjects)
                .Where(x => x.ClassStartTime.Date == DateTime.Now.Date
                && x.Subject.UsersSubjects.FirstOrDefault(y => y.UserId == userId).UserId == userId)
                .Select(x => new IndexSchoolClassVM
                {
                    Id = x.Id,
                    ClassStartTime = x.ClassStartTime,
                    ClassEndTime = x.ClassEndTime,
                    Code = x.Code,
                    Subject = x.Subject,
                    SubjectId = x.SubjectId,
                    Attendances = x.Attendances,
                    UsersInClass = x.Attendances.Count,
                    Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                    IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
                })
                .ToListAsync();
        }

        public async Task<EditSchoolClassVM> GetEditSchoolClassVM(int id)
        {
            return await this.context
                .SchoolClasses
                .Select(x => new EditSchoolClassVM
                {
                    Id = x.Id,
                    ClassStartTime = x.ClassStartTime,
                    ClassEndTime = x.ClassEndTime,
                    Subject = x.Subject,
                    SubjectId = x.SubjectId
                }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<IndexSchoolClassVM>> GetAllAsIndexSchoolClassesVm(string schoolClassSet)
        {
            var result = schoolClassSet switch
            {
                PastClasses => await this.context
                .SchoolClasses
                .Include(x => x.Subject)
                .Where(x => x.ClassEndTime < DateTime.Now)
                .OrderByDescending(x => x.ClassStartTime)
                .Select(x => new IndexSchoolClassVM
                {
                    Id = x.Id,
                    Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                    Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                    Code = x.Code,
                    Subject = x.Subject,
                    SubjectId = x.SubjectId,
                    Attendances = x.Attendances,
                    UsersInClass = x.Attendances.Count,
                    Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                    IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
                }).ToListAsync(),

                UpcomingClasses => await this.context
              .SchoolClasses
              .Include(x => x.Subject)
              .Where(x => x.ClassStartTime >= DateTime.Now)
              .OrderBy(x => x.ClassStartTime)
              .Select(x => new IndexSchoolClassVM
              {
                  Id = x.Id,
                  Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                  Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                  Code = x.Code,
                  Subject = x.Subject,
                  SubjectId = x.SubjectId,
                  Attendances = x.Attendances,
                  UsersInClass = x.Attendances.Count,
                  Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                  IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
              }).ToListAsync(),

                _ => await this.context
              .SchoolClasses
              .Include(x => x.Subject)
              .Where(x => x.ClassStartTime <= DateTime.Now && x.ClassEndTime >= DateTime.Now)
              .OrderBy(x => x.ClassStartTime)
              .Select(x => new IndexSchoolClassVM
              {
                  Id = x.Id,
                  Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                  Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                  Code = x.Code,
                  Subject = x.Subject,
                  SubjectId = x.SubjectId,
                  Attendances = x.Attendances,
                  UsersInClass = x.Attendances.Count,
                  Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                  IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
              }).ToListAsync()
            };

            return result;
        }

        public async Task<IEnumerable<IndexSchoolClassVM>> GetAllAsIndexSchoolClassesVmByUser(string userId, string schoolClassSet)
        {
            var result = schoolClassSet switch
            {
                PastClasses => await this.context
                .SchoolClasses
                .Include(x => x.Subject)
                .Where(x => x.ClassEndTime < DateTime.Now && x.Subject.UsersSubjects.FirstOrDefault(y => y.UserId == userId).UserId == userId)
                .OrderByDescending(x => x.ClassStartTime)
                .Select(x => new IndexSchoolClassVM
                {
                    Id = x.Id,
                    Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                    Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                    Code = x.Code,
                    Subject = x.Subject,
                    SubjectId = x.SubjectId,
                    Attendances = x.Attendances,
                    UsersInClass = x.Attendances.Count,
                    Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                    IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
                }).ToListAsync(),

                UpcomingClasses => await this.context
              .SchoolClasses
              .Include(x => x.Subject)
              .Where(x => x.ClassStartTime >= DateTime.Now && x.Subject.UsersSubjects.FirstOrDefault(y => y.UserId == userId).UserId == userId)
              .OrderBy(x => x.ClassStartTime)
              .Select(x => new IndexSchoolClassVM
              {
                  Id = x.Id,
                  Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                  Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                  Code = x.Code,
                  Subject = x.Subject,
                  SubjectId = x.SubjectId,
                  Attendances = x.Attendances,
                  UsersInClass = x.Attendances.Count,
                  Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                  IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
              }).ToListAsync(),

                _ => await this.context
              .SchoolClasses
              .Include(x => x.Subject)
              .Where(x => (x.ClassStartTime <= DateTime.Now && x.ClassEndTime >= DateTime.Now) && x.Subject.UsersSubjects.FirstOrDefault(y => y.UserId == userId).UserId == userId)
              .OrderBy(x => x.ClassStartTime)
              .Select(x => new IndexSchoolClassVM
              {
                  Id = x.Id,
                  Date = x.ClassStartTime.GetDateTimeFormats('D')[0],
                  Time = $"{x.ClassStartTime.TimeOfDay} - {x.ClassEndTime.TimeOfDay}",
                  Code = x.Code,
                  Subject = x.Subject,
                  SubjectId = x.SubjectId,
                  Attendances = x.Attendances,
                  UsersInClass = x.Attendances.Count,
                  Participants = x.Attendances.Where(a => a.CheckIn_Start == true || a.CheckIn_Middle == true || a.CheckIn_End == true).Count(),
                  IsCurrentClass = x.ClassStartTime <= DateTime.Now && x.ClassEndTime > DateTime.Now
              }).ToListAsync()
            };

            return result;
        }

        public async Task<bool> IsCheckInActive(int id)
        {
            var schoolClass = await this.context
                .SchoolClasses
                .FirstOrDefaultAsync(x => x.Id == id);

            bool notNull = DateTime.TryParse(schoolClass.CodeGeneratedTime.ToString(), out DateTime parsed);

            if (notNull)
            {
                return (DateTime.Now - parsed).Minutes < TimeToCheckIn;
            }

            return false;
        }


    }
}
