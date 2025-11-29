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

builder.Services.AddScoped<IUserCacheService, UserCacheService>();

builder.Services.AddDbContext<UserDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));
builder.Services.AddDbContext<CompanyDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<OOP2.Domain.Services.CompanyDomainService>();

builder.Services.AddScoped<OOP2.Domain.Repository.Company.ICompanyRepository, OOP2.Infrastructure.Repository.Company.CompanyRepository>();



builder.Services.AddScoped<OOP2.Application.Users.User.UserRequestHandler>();
builder.Services.AddScoped<OOP2.Application.Companys.Company.CompanyReqHandler>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDapperManager, DapperManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();