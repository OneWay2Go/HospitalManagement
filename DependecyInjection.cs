using HospitalManagement.DataAccess;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services.Doctors;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HospitalManagement
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddDbContext<HospitalContext>(options =>
            {
                options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention();
            });

            return services;
        }
    }
}
