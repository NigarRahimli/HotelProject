﻿using Autofac;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using FluentValidation.AspNetCore;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using Resume.Application.Services;


namespace Project.Application
{
    public class ApplicationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            base.Load(builder);
            builder.RegisterType<JwtService>()
                .As<IJwtService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CryptoService>()
                .As<ICryptoService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>()
                .As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FakeIdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ValidatorInterceptor>()
             .As<IValidatorInterceptor>()
             .SingleInstance();

            builder.RegisterType<FileService>()
                .As<IFileService>()
                .InstancePerLifetimeScope();
        }
    }
}
