namespace RollCall.MVC.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
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
                .Attendance.
                Include(a => a.Class)
                .Include(a => a.User)
                .ToListAsync();
        }

        public Task<Attendance> GetAttendance(int id)
        {
            return this.context
                .Attendance
                .Include(a => a.Class)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task UpdateAttendance(int id, DateTime dateTime, string userId, int classId)
        {
            throw new NotImplementedException();
        }
    }
}
