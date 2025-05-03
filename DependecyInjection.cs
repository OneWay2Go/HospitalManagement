using FluentValidation;
using FluentValidation.AspNetCore;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Middlewares;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using HospitalManagement.Services.Doctors;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Sats.PostgreSqlDistributedCache;
using System.Reflection;

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
            services.AddMemoryCache();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddDbContext<HospitalContext>(options =>
            {
                options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention();
            });

            services.AddPostgresDistributedCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379"; // Redis server
            });

            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 5;
                    options.Window = TimeSpan.FromSeconds(20);
                });

                //rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                //    RateLimitPartition.GetFixedWindowLimiter(
                //        partitionKey: httpContext.Connection.RemoteIpAddress.ToString(),
                //        factory: _ => new FixedWindowRateLimiterOptions
                //        {
                //            PermitLimit = 5,
                //            Window = TimeSpan.FromSeconds(10)
                //        }
                //    ));

                rateLimiterOptions.AddTokenBucketLimiter("tokenBucket", options =>
                {
                    options.TokenLimit = 5;
                    options.ReplenishmentPeriod = TimeSpan.FromSeconds(20);
                    options.TokensPerPeriod = 3;
                });
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddApiVersioning();

            return services;
        }

        public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalLoggingMiddleware>();
            app.UseMiddleware<ConfigurationValidationMiddleware>();
            app.UseMiddleware<CorrelationIdLoggingMiddleware>();

            return app;
        }
    }
}
