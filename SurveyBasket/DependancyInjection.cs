using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Presistence.DbContext;
using SurveyBasket.Presistence.Entities;

namespace SurveyBasket
{
    public static class DependancyInjection
    {

        static IServiceCollection AddDataBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Not founded as connectionstring");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            return services;
        }

        static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
