using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Project.Api.DI
{
    class ProjectServiceProviderFactory : AutofacServiceProviderFactory
    {
        public ProjectServiceProviderFactory()
            : base(OnRegister)
        {

        }
        private static void OnRegister(ContainerBuilder builder)
        {
            builder.RegisterModule<ProjectModule>();
        }
    }
}
