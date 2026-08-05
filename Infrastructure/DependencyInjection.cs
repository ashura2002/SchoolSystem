using Application.Interfaces;
using CloudinaryDotNet;
using Infrastructure.Data;
using Infrastructure.Events;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // repository and interfaces
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();

            services.AddScoped<ISchoolClassRepository, SchoolClassRepository>();
            services.AddScoped<ISchoolClassReadRepository, SchoolClassReadRepository>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentReadRespository, EnrollmentReadRepository>();

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileReadRepository, ProfileReadRepository>();
 
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // services
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // Bind config section
            services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.SectionName));
            // Register Cloudinary client
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;

                var account = new Account(
                    settings.CloudName,
                    settings.ApiKey,
                    settings.ApiSecret);

                return new Cloudinary(account);
            });


            // seeded data registration
            services.AddScoped<DatabaseSeeder>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IImageStorage, ImageStorageService>();
            return services;
        }

    }
}
