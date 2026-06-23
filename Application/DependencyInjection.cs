using Application.Services;
using Application.UseCases.Auth;
using Application.UseCases.Class.Admin;
using Application.UseCases.Class.Teacher;
using Application.UseCases.Users;
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
            // users and auth
            services.AddScoped<CreateAdminUserUseCase>();
            services.AddScoped<CreateStudentUseCase>();
            services.AddScoped<CreateTeacherUseCase>();
            services.AddScoped<DeleteUserUseCase>();
            services.AddScoped<GetByUsernameUseCase>();
            services.AddScoped<GetUserByEmailUseCase>();
            services.AddScoped<GetUserByIdUseCase>();
            services.AddScoped<UpdateUserUseCase>();
            services.AddScoped<GetAllActiveUsersUseCase>();
            services.AddScoped<CreateUserService>();
            services.AddScoped<LoginUseCase>();
            services.AddScoped<GetLoginUserUseCase>();
            services.AddScoped<GetAllDeactiveUsers>();

            // school class
            services.AddScoped<CreateSchoolClassUseCase>();
            services.AddScoped<AssignTeacherUseCase>();
            services.AddScoped<GetAllClassUseCase>();
            services.AddScoped<GetClassesWithoutTeacher>();
            services.AddScoped<GetAllClassesWithTeacher>();
            services.AddScoped<GetClassByIdUseCase>();
            services.AddScoped<GetOwnClasses>();
            services.AddScoped<UpdateClassUseCase>();
            return services;
        }

    }
}
