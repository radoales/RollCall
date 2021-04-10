namespace RollCall.MVC.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.Services;
    using RollCall.MVC.Services.Implementations;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<RollCallDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.
                  AddTransient<IIdentityService, IdentityService>()
                  .AddTransient<IAttendanceService, AttendanceService>()
                  .AddTransient<ISubjectServices, SubjectService>()
                  .AddTransient<ISchoolClassService, SchoolClassService>()
                  .AddTransient<IUserService, UserService>();


            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
            })
               .AddEntityFrameworkStores<RollCallDbContext>();

            return services;
        }

        public static IServiceCollection AddDefaultLoginPath(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/LogIn");

            return services;
        }

        //public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(Startup));

        //    return services;
        //}
    }
}
