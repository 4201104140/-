using IdentityServer.EntityFramework.Storage;
using IdentityServerDb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cn = builder.Configuration.GetConnectionString("db");

builder.Services.AddOperationalDbContext(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(cn, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
});

builder.Services.AddConfigurationDbContext(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(cn, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
SeedData.EnsureSeedData(app.Services);
//app.Run();
