using Microsoft.EntityFrameworkCore;
using OOP2.Application.Users.User;
using OOP2.Domain.Repository.Company;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Repository.User;
using OOP2.Infrastructure.Database.Dapper;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<UserDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<UserRequestHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDapperManager, DapperManager>();
builder.Services.AddScoped<DapperManager>();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
