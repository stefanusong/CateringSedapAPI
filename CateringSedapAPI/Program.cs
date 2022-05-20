using CateringSedapAPI.Context;
using CateringSedapAPI.Services;
using CateringSedapAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using CateringSedapAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add controller
builder.Services.AddControllers();

// Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register database
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbCredentials"))
);

// Register dependency injection
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IResponseHelper, ResponseHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
