namespace RollCall.MVC.Services.Implementations
{
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
    public class SchoolClassService : ISchoolClassService
    {
        private readonly RollCallDbContext context;
        private readonly ISubjectServices subjectServices;

        public SchoolClassService(RollCallDbContext context, ISubjectServices subjectServices)
        {
            this.context = context;
            this.subjectServices = subjectServices;
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
                  DateTime.Now < endBlock1 ? 1
                : DateTime.Now > endBlock1 && DateTime.Now < endBlock2 ? 2
                : DateTime.Now > endBlock2 && DateTime.Now < endBlock3 ? 3
                : 0;

            return currentBlock;
        }

        public Task Delete(SchoolClass schoolClass)
        {
            throw new NotImplementedException();
        }

        public async Task GenerateCode(int id)
        {
            var schoolClass = await this.context
                .SchoolClasses
                .FirstOrDefaultAsync(x => x.Id == id);

            var random = new Random();

            schoolClass.Code = random.Next(1000, 9999);
            schoolClass.CodeGeneratedTime = DateTime.Now;

            this.context.Update(schoolClass);
            await this.context.SaveChangesAsync();
        }

        public async Task<DetailsSchoolClassVM> Get(int id)
        {
            var currentBlock = await GetCurrentBlock(id);

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
                     }).ToList(),
                     Code = x.Code,
                     SubjectId = x.SubjectId,
                     CodeGeneratedTime = x.CodeGeneratedTime.Value.AddMinutes(30).ToString("MMM d, yyyy HH':'mm':'ss"),
                     Subject = x.Subject,
                     TimeLeft = x.CodeGeneratedTime != null ? (DateTime)x.CodeGeneratedTime : null
                 })
                 .FirstOrDefaultAsync(x => x.Id == id);



            return result;
        }

        public async Task<DetailsSchoolClassVM> GetAsStudent(int id, string userId)
        {
            var currentBlock = await GetCurrentBlock(id);

            return await this.context.SchoolClasses
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
                     }).ToList(),
                     Code = x.Code,
                     SubjectId = x.SubjectId,
                     CodeGeneratedTime = x.CodeGeneratedTime.Value.AddMinutes(30).ToString("MMM d, yyyy HH':'mm':'ss"),
                     Subject = x.Subject,
                     TimeLeft = x.CodeGeneratedTime != null ? (DateTime)x.CodeGeneratedTime : null
                 })
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<IndexSchoolClassVM>> GetAll()
        {
            return await this.context
                .SchoolClasses
                .Include(x => x.Subject)
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
                }).ToListAsync();
        }

        public Task Update(int id, DateTime dateTime, string userId, int classId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IndexSchoolClassVM>> GetTodaysLoggedInUserClasses(string userId)
        {
            return await this.context
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
                })
                .ToListAsync();
        }

        private async Task<double> CalculateAttendancePercentage(string userId, int classId)
        {
            var attendance = await this.context.Attendances.FirstOrDefaultAsync(x => x.UserId == userId && x.ClassId == classId);
            var list = new List<bool>() { attendance.CheckIn_Start, attendance.CheckIn_Middle, attendance.CheckIn_End };
            double count = Math.Floor(list.Count(x => x == true) / 3.00 * 100);
            return count;
        }
    }
}
