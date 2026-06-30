using Application.Features.Auth.Commands;
using Application.Features.Auth.Queries;
using Application.Features.Auth.Services;
using Application.Features.Class.Admin.Commands;
using Application.Features.Class.Admin.Queries;
using Application.Features.Class.Teacher.Queries;
using Application.Features.Enrollments.Admin.Commands;
using Application.Features.Enrollments.Admin.Queries;
using Application.Features.Enrollments.Student.Commands;
using Application.Features.Enrollments.Student.Queries;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Features.Users.Services;
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
            services.AddScoped<CreateAdminUserHandler>();
            services.AddScoped<CreateStudentHandler>();
            services.AddScoped<CreateTeacherHandler>();
            services.AddScoped<DeleteUserHandler>();
            services.AddScoped<GetByUsernameHandler>();
            services.AddScoped<GetUserByEmailHandler>();
            services.AddScoped<GetUserByIdHandler>();
            services.AddScoped<UpdateUserHandler>();
            services.AddScoped<GetAllActiveUsersUseCase>();
            services.AddScoped<CreateUserService>();
            services.AddScoped<LoginHandler>();
            services.AddScoped<GetLoginUserHandler>();
            services.AddScoped<GetAllDeactiveUsersHandler>();

            // school class
            services.AddScoped<CreateSchoolClassHandler>();
            services.AddScoped<AssignTeacherHandler>();
            services.AddScoped<RemoveTeacherHandler>();
            services.AddScoped<GetAllClassHandler>();
            services.AddScoped<GetClassesWithoutTeacherHandler>();
            services.AddScoped<GetAllClassesWithTeacherHandler>();
            services.AddScoped<GetClassByIdHandler>();
            services.AddScoped<GetTeacherOwnClassesHandler>();
            services.AddScoped<UpdateClassHandler>();
            services.AddScoped<DeleteClassHandler>();
            services.AddScoped<GetTeacherClassByIdHandler>();

            // enrollment
            services.AddScoped<RequestEnrollmentHandler>();
            services.AddScoped<GetAllPendingEnrollmentsHandler>();
            services.AddScoped<ApproveEnrollmentHandler>();
            services.AddScoped<RejectEnrollmentHandler>();
            services.AddScoped<GetAllMyClassesHandler>();
            services.AddScoped<CancelEnrollmentHandler>();
            services.AddScoped<DropEnrollmentHandler>();
            services.AddScoped<GetMyClassByIdhandler>();
            return services;
        }

    }
}
