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
