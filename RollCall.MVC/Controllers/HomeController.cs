namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ISchoolClassService schoolClassService;
        public HomeController(ISchoolClassService schoolClassService)
        {
            this.schoolClassService = schoolClassService;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await this.schoolClassService.GetAll();
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
