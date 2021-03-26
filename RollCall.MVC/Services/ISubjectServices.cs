namespace RollCall.MVC.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubjectServices
    {
        SelectList GetSubjectsAsSelectedList();
        Task<IEnumerable<User>> GetUsersInSubject(int id);
    }
}
