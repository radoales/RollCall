namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.Infrastructure;
    using RollCall.MVC.Services;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISchoolClassService schoolClassService;
        private readonly UserManager<User> userManager;
        public HomeController(ISchoolClassService schoolClassService, UserManager<User> userManager)
        {
            this.schoolClassService = schoolClassService;
            this.userManager = userManager;
        }

        [ServiceFilter(typeof(ClientIpCheckActionFilter))]
        public async Task<IActionResult> Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var classes = this.User.IsInRole(Roles.AdminRole) ? await this.schoolClassService.GetTodaysClasses()
                : await this.schoolClassService.GetTodaysLoggedInUserClasses(userId);
            return View(classes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
