using Autofac;
using Project.Application;
using Project.DataAccessLayer;
using Project.Repository;

namespace Project.Api.AppCode.DI
{
    class ProjectModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.RegisterModule<DataAccessModule>();
            //more than one
            builder.RegisterAssemblyModules(typeof(DataAccessModule).Assembly);
            builder.RegisterAssemblyModules(typeof(ApplicationModule).Assembly);

            builder.RegisterAssemblyTypes(typeof(IRepositoryReference).Assembly)
                .AsImplementedInterfaces();

        }
    }
}
