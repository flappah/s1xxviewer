using Autofac;
using System.Linq;

namespace S1xxViewer.Storage
{
    public class HandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .As(type => type.GetInterfaces())
                .Where(tp =>
                    tp.Name.ToUpper().EndsWith("STORAGE"))
                .InstancePerDependency();
        }
    }
}
