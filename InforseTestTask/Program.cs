using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Infastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using InforseTestTask.Core.Domain.Repositories;
using InforseTestTask.Infastructure.Repositories;
using InforseTestTask.Core.Services;
using InforseTestTask.Core.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InforseDBContext>(options =>
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
    .EnableSensitiveDataLogging(true)
    .EnableDetailedErrors()
    );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>());
        policyBuilder.WithHeaders("Authorization", "content-type", "origin", "accept");
        policyBuilder.WithMethods("GET", "POST", "DELETE", "PUT");
        //policyBuilder.WithOrigins("http://localhost:4200");
    });
});


builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
builder.Services.AddScoped<IAboutInfoRepository, AboutInfoRepository>();

builder.Services.AddScoped<IShortUrlService, ShortUrlService>();

builder.Services.AddTransient<IJwtService, JwtServiceImpl>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
           System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<InforseDBContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole,
    InforseDBContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole,
    InforseDBContext, Guid>>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
