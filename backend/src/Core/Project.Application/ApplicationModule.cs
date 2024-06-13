using Autofac;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application
{
    public class ApplicationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FakeIdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();
         
        }
    }
}
