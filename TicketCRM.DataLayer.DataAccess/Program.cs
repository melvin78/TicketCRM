using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketCRM.DataAccess.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo {Title = "Centrino.Email.Integrations.DataAccess", Version = "v1"});
});


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TicketCRMDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion)
        .EnableSensitiveDataLogging() // <-- These two calls are optional but help
        .EnableDetailedErrors());

app.Run();