using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services;
using OOP2.Domain.Services.Cache;
using OOP2.Infrastructure.Cache;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Database.Dapper;
using OOP2.Infrastructure.Repository.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

// ? ovo fali
builder.Services.AddScoped<IUserCacheService, UserCacheService>();

builder.Services.AddDbContext<UserDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddScoped<UserDomainService>();

// ? registriramo konkretan handler (Clean Architecture friendly)
builder.Services.AddScoped<OOP2.Application.Users.User.UserRequestHandler>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDapperManager, DapperManager>();

// ? ovo ti NE treba posebno jer je veæ registriran preko IDapperManager
// builder.Services.AddScoped<DapperManager>(); (možeš obrisati ako želiš minimalno)

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