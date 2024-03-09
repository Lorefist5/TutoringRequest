using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TutoringRequest.Api.Helpers.TokenHelpers;
using TutoringRequest.Api.Mapping;
using TutoringRequest.Data;
using TutoringRequest.Data.Repositories.DatabaseRepositories;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Enums;
using TutoringRequest.Services.Interfaces;
using TutoringRequest.Services.MessagingServices;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles));
builder.Services.AddDbContext<TutoringDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Test"));
});
//Run 
//dotnet user-secrets set "EmailSettings:Email" "your-email@gmail.com"
//dotnet user-secrets set "EmailSettings:Password" "your-app-password"
//That way you can send emails with your gmail account
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<ResetTokenGenerator>();
builder.Services.AddScoped<IResetTokenRepository, ResetTokenRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IResetTokenRepository, ResetTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(DefaultRoles.Admin.ToString()));
});

builder.Services.AddScoped<TokenGenerator>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();