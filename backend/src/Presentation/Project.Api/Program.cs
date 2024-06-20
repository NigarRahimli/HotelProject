using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Project.Api.DI;
using Project.Application;


var builder = WebApplication.CreateBuilder(args);
//bax nedi
builder.Host.UseServiceProviderFactory(new ProjectServiceProviderFactory());
builder.Services.AddControllers();

builder.Services.AddDbContext<DbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
    {

        opt.MigrationsHistoryTable("MigrationHistory");
    });
});

builder.Services.AddFluentValidationAutoValidation(cfg => cfg.DisableDataAnnotationsValidation = false);
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationModule>(includeInternalTypes: true);
builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblyContaining<ApplicationModule>());
builder.Services.AddAutoMapper(typeof(ApplicationModule).Assembly);

var app = builder.Build();
app.MapControllers();

app.Run();
