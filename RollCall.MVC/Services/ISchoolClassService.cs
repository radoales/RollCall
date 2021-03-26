namespace RollCall.MVC.Services
{
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISchoolClassService
    {
        Task<IEnumerable<IndexSchoolClassVM>> GetAll();
        Task<DetailsSchoolClassVM> Get(int id);
        Task Create(DateTime classStartTime, DateTime classEndTime, int subjectId);
        Task Update(int id, DateTime dateTime, string userId, int classId);
        Task Delete(SchoolClass schoolClass);
        Task GenerateCode(int id);
    }
}
