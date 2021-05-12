namespace RollCall.MVC.Services
{
    using Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAll();
        Task<Attendance> GetAttendance(int id);
        Task CreateAttendance(string userId, int classId);
        Task UpdateAttendance(int id, DateTime dateTime, string userId, int classId);
        Task DeleteAttendance(Attendance attendance);
        Task CheckIn(string userId, int classId, int spot);
        Task<bool> IsStudentCheckedInforCurrentBlock(string userId, int schoolClassId, int currentBlock);
        Task<bool> HasUserPassedAttendancesInSubject(string userId, int subjectId);
        Task<List<ListAtendanceVM>> GetSchoolClassAttendances(int classId, string searchString);
    }
}
