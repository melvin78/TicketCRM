using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.EmailService;
using IdentityServerAspNetIdentity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketCRM;
using TicketCRM.DataAccess;
using TicketCRM.DataAccess.Configuration;
using TicketCRM.DataLayer.EmailTemplates.Services;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.SupportModule;
using AgentService = TicketCRM.SupportModule.AgentService;
using DepartmentService = TicketCRM.SupportModule.DepartmentService;
using IAgentService = TicketCRM.SupportModule.IAgentService;
using IDepartmentService = TicketCRM.SupportModule.IDepartmentService;
using IOrganizationService = TicketCRM.SupportModule.IOrganizationService;
using OrganizationService = TicketCRM.SupportModule.OrganizationService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
EmailSettings emailSettings = new EmailSettings();
configuration.GetSection("EmailSettings").Bind(emailSettings);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));


//->Main Services
builder.Services.AddControllers();
builder.Services.AddRazorPages();

//->Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "Centrino.ApplicationLayer.MainBoundedContext", Version = "v1" });
});

//->Db context registration



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection") ?? string.Empty, serverVersion)
        .EnableSensitiveDataLogging() // <-- These two calls are optional but help
        .EnableDetailedErrors());

builder.Services.AddDbContext<TicketCRMDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection") ?? string.Empty, serverVersion)
        .EnableSensitiveDataLogging() // <-- These two calls are optional but help
        .EnableDetailedErrors());


//->Application services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<EmailSettings>(es => emailSettings);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddTransient(typeof(IEmailTemplateResolver<>), typeof(EmailTemplateResolver<>));
builder.Services.AddTransient<IEmailService, EmailService>(email=>new EmailService(emailSettings));
builder.Services.AddScoped<IPusherService, PusherService>();
builder.Services.AddTransient<IEnquiriesService, EnquiriesService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IEnquiryCategoryService, EnquiryCategoryService>();
builder.Services.AddTransient<IOrganizationService,OrganizationService>();
builder.Services.AddTransient<ITicketStatusService,TicketStatusService>();
builder.Services.AddTransient<ITicketService,TicketService>();
builder.Services.AddTransient<IReportClientService, ReportClientService>();
builder.Services.AddTransient<IReportAgentService, ReportAgentService> ();
builder.Services.AddTransient<IAssignTicketService, AssignTicketService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IResponseService, ResponseService>();
builder.Services.AddTransient<ITicketAssignmentService, TicketAssignmentService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<IRecentActivityService, RecentActivityService>();
builder.Services.AddTransient<IPriorityLevelService, PriorityLevelService>();
builder.Services.AddTransient<IInboxService, InboxService>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IManualTicketAssignment, ManualTicketAssignment>();
builder.Services.AddTransient<IOrganizationService, OrganizationService>();





var app = builder.Build();

// Configure the HTTP request pipeline.


if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Crm Support Portal"));
}

app.UseHttpsRedirection();
app.UseCors("default");

            
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();