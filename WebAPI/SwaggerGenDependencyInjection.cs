using Microsoft.OpenApi;

namespace WebAPI
{
    public static class SwaggerGenDependencyInjection
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "School System",
                    Description = """
                        A RESTful ASP.NET Core Web API built using Clean Architecture, Domain-Driven Design (DDD),
                        and CQRS to demonstrate a scalable, maintainable, and production-oriented backend architecture.
                        Features include JWT Authentication, Role-Based Authorization, Entity Framework Core with PostgreSQL, 
                        Cloudinary image storage integration, Serilog structured logging, global exception handling, and pagination support.
                        """,
                    Version = "v1"
                });

                opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                opt.AddSecurityRequirement(docs => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearer", docs)] = new List<string>()
                });
            }); ;


            return services;
        }
    }
}
