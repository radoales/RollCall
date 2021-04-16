namespace RollCall.MVC.Data.DataSeed
{
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;
    public class Seed
    {
        private readonly UserManager<User> userManager;
        private readonly RollCallDbContext context;

        public Seed(UserManager<User> userManager, RollCallDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedStudents()
        {

            var userData = System.IO.File.ReadAllText("Data/DataSeed/Students.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "11111111");
                await userManager.AddToRoleAsync(user, Roles.StudentRole);

            }

        }
    }
}
