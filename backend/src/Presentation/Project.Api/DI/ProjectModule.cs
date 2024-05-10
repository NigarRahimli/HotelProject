using Autofac;
using Project.DataAccessLayer;

namespace Project.Api.DI
{
     class ProjectModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.RegisterModule<DataAccessModule>();
            //more than one
            builder.RegisterAssemblyModules(typeof(DataAccessModule).Assembly);

        }
    }
}
