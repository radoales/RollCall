namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class AttendanceService : IAttendanceService
    {
        private readonly RollCallDbContext context;

        public AttendanceService(RollCallDbContext context)
        {
            this.context = context;
        }

        public async Task CheckIn(string userId, int classId, int currentBlock )
        {
            var attendance = await this.context
                .Attendances.FirstOrDefaultAsync(x => x.ClassId == classId && x.UserId == userId);

            switch (currentBlock)
            {
                case 1: attendance.CheckIn_Start = true;
                    break;
                case 2: attendance.CheckIn_Middle = true;
                    break;
                case 3: attendance.CheckIn_End = true;
                    break;
                default:
                    break;
            }

            this.context.Update(attendance);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateAttendance(string userId, int classId)
        {
            var attendance = new Attendance
            {
                ClassId = classId,
                UserId = userId
            };

            this.context.Attendances.Add(attendance);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAttendance(Attendance attendance)
        {
             this.context.Attendances.Remove(attendance);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAll()
        {
            return await this.context
                .Attendances.
                Include(a => a.Class)
                .Include(a => a.User)
                .ToListAsync();
        }

        public Task<Attendance> GetAttendance(int id)
        {
            return this.context
                .Attendances
                .Include(a => a.Class)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        } 

        public async Task<List<ListAtendanceVM>> GetSchoolClassAttendances(int classId, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.context
              .Attendances
              .Where(x => x.ClassId == classId )
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
              })
              .OrderBy(x => x.User.FirstName)
              .ToListAsync();
            }

            return await this.context 
                .Attendances
                .Where(x => x.ClassId == classId && (x.User.FirstName.StartsWith(searchString) || x.User.LastName.StartsWith(searchString)))
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
                })
                .ToListAsync();
        }

        public async Task<bool> HasUserPassedAttendancesInSubject(string userId, int subjectId)
        {
            return await this.context
                .Attendances
                .AnyAsync(x => x.UserId == userId && x.Class.SubjectId == subjectId && x.Class.ClassStartTime.Date < DateTime.Now.Date);
        }

        public async Task<bool> IsStudentCheckedInforCurrentBlock(string userId, int schoolClassId, int currentBlock)
        {
            var attendance = await this.context
                .Attendances
                .FirstOrDefaultAsync(x => x.ClassId == schoolClassId && x.UserId == userId);
            bool result = false;

            switch (currentBlock)
            {
                case 1: result = attendance.CheckIn_Start == true;
                    break;
                case 2:
                    result = attendance.CheckIn_Middle == true;
                    break;
                case 3:
                    result = attendance.CheckIn_End == true;
                    break;
                default:
                    break;
            }

            return result;
        }

        public Task UpdateAttendance(int id, DateTime dateTime, string userId, int classId)
        {
            throw new NotImplementedException();
        }
    }
}
