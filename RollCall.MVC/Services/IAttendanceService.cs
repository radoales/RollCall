namespace RollCall.MVC.Services
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAll();
        Task<Attendance> GetAttendance(int id);
        Task CreateAttendance(DateTime dateTime, string userId, int classId);
        Task UpdateAttendance(int id, DateTime dateTime, string userId, int classId);
        Task DeleteAttendance(Attendance attendance);
    }
}
