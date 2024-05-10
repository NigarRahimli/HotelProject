using Microsoft.EntityFrameworkCore;
using Project.Api.DI;

var builder = WebApplication.CreateBuilder(args);
//bax nedi
builder.Host.UseServiceProviderFactory(new ProjectServiceProviderFactory());

builder.Services.AddDbContext<DbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
    {

        opt.MigrationsHistoryTable("MigrationHistory");
    });
});

var app = builder.Build();

app.Run();
