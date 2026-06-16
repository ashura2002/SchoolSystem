using Application.Services;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<CreateAdminUserUseCase>();
            services.AddScoped<CreateStudentUseCase>();
            services.AddScoped<CreateTeacherUseCase>();
            services.AddScoped<DeleteUserUseCase>();
            services.AddScoped<GetByUsernameUseCase>();
            services.AddScoped<GetUserByEmailUseCase>();
            services.AddScoped<GetUserByIdUseCase>();
            services.AddScoped<UpdateUserUseCase>();
            services.AddScoped<GetAllUsersUseCase>();
            services.AddScoped<CreateUserService>();
            services.AddScoped<LoginUseCase>();
            return services;
        }

    }
}
