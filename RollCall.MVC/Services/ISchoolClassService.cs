namespace RollCall.MVC.Services
{
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISchoolClassService
    {
        Task<IEnumerable<IndexSchoolClassVM>> GetAllAsIndexSchoolClassesVmByUser(string userId, string schoolClassSet);
        Task<DetailsSchoolClassVM> GetDetailsSchoolClassVM(int id);
        Task<EditSchoolClassVM> GetEditSchoolClassVM(int id);
        Task<DetailsSchoolClassVM> GetAsStudent(int id, string userId);
        Task Create(DateTime classStartTime, DateTime classEndTime, int subjectId);
        Task Update(int id, DateTime startTime, DateTime endTime, int subjectId);
        Task Delete(int id);
        Task GenerateCode(int id);
        Task<IEnumerable<IndexSchoolClassVM>> GetTodaysClasses();
        Task<int> GetCurrentBlock(int classId);
        Task<IEnumerable<IndexSchoolClassVM>> GetTodaysLoggedInUserClasses(string userId);
        Task<bool> IsCheckInActive(int id);
        Task<IEnumerable<IndexSchoolClassVM>> GetAllAsIndexSchoolClassesVm(string schoolClassSet);
    }
}
