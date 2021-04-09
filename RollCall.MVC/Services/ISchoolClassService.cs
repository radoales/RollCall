namespace RollCall.MVC.Services
{
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISchoolClassService
    {
        Task<IEnumerable<IndexSchoolClassVM>> GetAllAsIndexSchoolClassesVmByUser(string userId);
        Task<IEnumerable<IndexSchoolClassVM>> GetUpcomingAsIndexSchoolClassesVmByUser(string userId);
        Task<IEnumerable<IndexSchoolClassVM>> GetPassedAsIndexSchoolClassesVmByUser(string userId);
        Task<DetailsSchoolClassVM> GetDetailsSchoolClassVM(int id);
        Task<EditSchoolClassVM> GetEditSchoolClassVM(int id);
        Task<DetailsSchoolClassVM> GetAsStudent(int id, string userId);
        Task Create(DateTime classStartTime, DateTime classEndTime, int subjectId);
        Task Update(int id, DateTime startTime, DateTime endTime, int subjectId);
        Task Delete(int id);
        Task GenerateCode(int id);
        Task<int> GetCurrentBlock(int classId);
        Task<IEnumerable<IndexSchoolClassVM>> GetTodaysLoggedInUserClasses(string userId);
    }
}
