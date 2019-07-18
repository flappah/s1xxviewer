using Autofac;
using S1xxViewer.Types.Interfaces;
using System.Linq;

namespace S1xxViewer.Types
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .As(type => type.GetInterfaces())
                .Where(tp =>
                    tp.GetInterfaces().Contains(typeof(IFeature)))
                .InstancePerLifetimeScope();
        }
    }
}
