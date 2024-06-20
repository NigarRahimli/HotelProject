using Autofac;
using FluentValidation.AspNetCore;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using NetTopologySuite.Geometries;
using NetTopologySuite;


namespace Project.Application
{
    public class ApplicationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326))
                .As<GeometryFactory>()
                .SingleInstance();



            base.Load(builder);
            builder.RegisterType<FakeIdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ValidatorInterceptor>()
             .As<IValidatorInterceptor>()
             .SingleInstance();

        }
    }
}
