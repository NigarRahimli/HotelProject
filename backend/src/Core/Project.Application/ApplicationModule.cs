using Autofac;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Resume.Application.Services;
using Project.Infrastructure.Common;

namespace Project.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<JwtService>()
                .As<IJwtService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CryptoService>()
                .As<ICryptoService>()
                .SingleInstance();

            builder.RegisterType<StripePaymentService>()
             .As<IStripePaymentService>()
             .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>()
                .As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SMSService>()
                .As<ISMSService>()
                .InstancePerLifetimeScope();



            builder.RegisterType<ValidatorInterceptor>()
                .As<IValidatorInterceptor>()
                .SingleInstance();

            builder.RegisterType<FileService>()
                .As<IFileService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>()
                .As<IActionContextAccessor>()
                .SingleInstance();
          
        }
    }
}
