using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data;
using TutoringRequest.Data.Repositories;
using TutoringRequest.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TutoringDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Test"));
});
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
