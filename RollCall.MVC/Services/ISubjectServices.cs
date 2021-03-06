namespace RollCall.MVC.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Subjects;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubjectServices
    {
        SelectList GetSubjectsAsSelectedList();
        Task<IEnumerable<User>> GetUsersInSubject(int id);
        Task<IEnumerable<AddUsersToSubjectVM>> GetAddUsersToSubjectVM(string name, int subjectId);
        Task<IEnumerable<User>> GetUsersNotInSubject(int id);
        Task<Subject> Get(int id);
        Task AddUserToSubject(string userId, int subjectId);
        Task RemoveUserFromSubject(string userId, int subjectId);
        Task<IEnumerable<Subject>> GetAllSubjectsByUser(string userId, string slot);
        SelectList GetUsersSubjectsAsSelectedList(string loggedInUserId, string userForDetailId);
        Task<IEnumerable<Subject>> GetAllSubjects(string slot);
        Task<bool> HasClassessOrUsers(int id);
        Task<DetailsSubjectVM> GetIndexSubjectVM(int id);
        Task<int> Create(string name);
        Task<int> CreateMany(List<CreateSubjectListVM> subjects);
        Task Update(int id, string name);
        Task Delete(int id);
        Task<bool> IsSubjectExisting(string name);
        public Task<double> GetAverageSubjectAttendance(int id);
    }
}
