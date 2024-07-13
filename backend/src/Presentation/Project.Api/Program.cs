using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Project.Api.AppCode.DI;
using Project.Api.AppCode.Pipeline;
using Project.Application;
using Project.Application.Services;
using Project.DataAccessLayer;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Common;
using Project.Infrastructure.Configurations;
using Resume.Api.AppCode.Pipeline;
using Project.DataAccessLayer.Contexts;

var builder = WebApplication.CreateBuilder(args);
//bax nedi
builder.Services.AddHttpContextAccessor();
builder.Host.UseServiceProviderFactory(new ProjectServiceProviderFactory());
builder.Services.AddCors(cfg =>
{

    cfg.AddPolicy("allowAll", p =>
    {

        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowAnyOrigin();

    });

});
builder.Services.AddControllers();

builder.Services.AddDbContext<DbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
    {
        opt.MigrationsHistoryTable("MigrationHistory");
    });
});

builder.Services.AddControllers(cfg =>
{
    var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();

    cfg.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(HeaderBinderBehaviour<,>));
builder.Services.Configure<EmailServiceOptions>(cfg => builder.Configuration.Bind(nameof(EmailServiceOptions), cfg))
                .AddSingleton<IEmailService, EmailService>();

builder.Services.Configure<CryptoServiceOptions>(cfg => builder.Configuration.Bind(nameof(CryptoServiceOptions), cfg))
      .AddSingleton<ICryptoService, CryptoService>();
builder.Services.Configure<StripeServiceOptions>(cfg => builder.Configuration.Bind(nameof(StripeServiceOptions), cfg));
builder.Services.Configure<SmsServiceOptions>(cfg => builder.Configuration.Bind(nameof(SmsServiceOptions), cfg));


builder.Services.AddCustomIdentity(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation(cfg => cfg.DisableDataAnnotationsValidation = false);
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationModule>(includeInternalTypes: true);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationModule>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DataAccessModule>());
builder.Services.AddAutoMapper(typeof(ApplicationModule).Assembly);

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("allowAll");
//app.UseErrorHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.SeedUser(builder.Configuration);


app.Run();
