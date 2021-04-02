using Newtonsoft.Json;
using RollCall.MVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollCall.MVC.Data.DataSeed
{
    public class Seed
    {
        public static void SeedUsers(RollCallDbContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/DataSeeds/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                var usersToAdd = new List<User>();

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    usersToAdd.Add(user);
                }

                context.AddRange(usersToAdd);
                context.SaveChanges();
            }
        }
    }
}
