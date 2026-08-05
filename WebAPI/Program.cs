using Application;
using Infrastructure;
using Infrastructure.Data;
using Serilog;
using System.Text;
using WebAPI;
using WebAPI.Middlewares;

// PHASE 1 construction phase
var builder = WebApplication.CreateBuilder(args);

//service registrations

//serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// Cors Policy
builder.Services.AddCorsPolicy();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerDocumentation();

// dependencies
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddJwtAuthenticationDI(builder.Configuration);

// for rate limiting 
builder.Services.AddRateLimiting();


// PHASE 2 build all the constructed services
var app = builder.Build();


// Seed the database with default data when the application starts.
// If the seeded admin already exists, nothing will be added.
using (var scope = app.Services.CreateScope()) {
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}


// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();



// middleware pipeline
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// PHASE 3 run the build application
app.Run();