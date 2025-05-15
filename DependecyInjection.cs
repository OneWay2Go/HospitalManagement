using FluentValidation;
using FluentValidation.AspNetCore;
using HospitalManagement.Application.Behaviors;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Middlewares;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using HospitalManagement.Services.Auth;
using HospitalManagement.Services.Doctors;
using HospitalManagement.Services.Hasher;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sats.PostgreSqlDistributedCache;
using System.Reflection;
using System.Text;

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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

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

        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "hospital.uz",
                    ValidAudience = "hospital.uz",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x4p8z7Kk1AzpGUZLt/7Bpe0TVI9k2fYenUQvzXOlzXiHugkE3HQY2lCqyMqBA39YoHGPufNqNEFH7MRo6MPoM6gie60yjbjrZJ5b3YTw08ZFbmi0x5yvZlvCK/+NiOXftdWWKmwHCsuGTl0HE47Kaw1Ods9VhzoWLLdBEFbgvMsN1UjoajBZ1/vhch2UKw/MVAgferF0e5CT8Auvsm742eEKcuON8HLCm1guxkRS9xERNYFiVyGdZxthSQ9qGtX2U3SGj03OCl+ZiUgI87e/e16xBNmAZ8gndG3248Dbgm4kzDq6kzezW3gmkQksfhnBKUalg4JVSM1zNFoaRdzzCLpqa00F5NCgiUvzjICwDSM=\r\n"))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(RoleType.Doctor), policy => policy.RequireClaim(nameof(RoleType.Doctor)));
                options.AddPolicy(nameof(RoleType.Patient), policy => policy.RequireClaim(nameof(RoleType.Patient)));
            });

            return services;
        }
    }
}
