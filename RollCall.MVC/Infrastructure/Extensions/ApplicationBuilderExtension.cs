namespace RollCall.MVC.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Data.Models;
    using System.Threading.Tasks;
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatasaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<RollCallDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                //var addressService = serviceScope.ServiceProvider.GetService<IAddressService>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.Roles.AdminRole;

                        var roles = new[]
                        {
                            adminName,
                            WebConstants.Roles.StudentRole,
                            WebConstants.Roles.TeacherRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExist = await roleManager.RoleExistsAsync(role);

                            if (!roleExist)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminMail = "admin@some.com";

                        var adminUser = await userManager.FindByNameAsync(adminMail);

                        if (adminUser == null)
                        {
                            //var address = new Address
                            //{
                            //    Town = "Copenhagen",
                            //    Zip = 2300,
                            //    AddressField = "Østrigsgade 7,3"
                            //};

                           // var addressId = await addressService.CreateAddress("Copenhagen", 2300, "Østrigsgade 7,3");

                            adminUser = new User
                            {
                                Email = adminMail,
                                UserName = "admin",
                                PhoneNumber = "12345678",
                                FirstName = "Rado",
                                LastName = "Naydenov"
                              //  AddressId = addressId

                            };

                            await userManager.CreateAsync(adminUser, "222222");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }

                    })
                    .Wait();


                return app;
            }
        }
    }
}
