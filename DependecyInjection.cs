using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Middlewares;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using HospitalManagement.Services.Doctors;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HospitalManagement
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.Configure<DoctorsSettings>(configuration.GetSection("DoctorsSettings"));
            services.Configure<AppointmentSettings>(configuration.GetSection("AppointmentSettings"));
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddTransient<ConfigurationValidationMiddleware>();
            services.AddTransient<CorrelationIdLoggingMiddleware>();

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
