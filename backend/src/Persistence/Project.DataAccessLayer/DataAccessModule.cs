using Autofac;
using Microsoft.EntityFrameworkCore;
using Project.DataAccessLayer.Contexts;

namespace Project.DataAccessLayer;

    public class DataAccessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<DataContext>().
            As<DbContext>()
            .InstancePerLifetimeScope();
    }

}

