using Autofac;
using System.IO;
using System.Linq;
using System.Reflection;
using S1xxViewer.Model;
using S1xxViewer.Model.Interfaces;
using System;
using System.Collections.Generic;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewerWPF
{
    public static class AutofacInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IContainer Initialize()
        {
            var allAssemblyNames =
                Directory.GetFiles(Directory.GetCurrentDirectory(), "S1xxViewer.*.dll", SearchOption.TopDirectoryOnly).ToList();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(allAssemblyNames.Select(Assembly.LoadFile).ToArray());

            var assemblyName = allAssemblyNames.Find(nm => nm.Contains(".Types.dll"));
            var typeAssembly = Assembly.LoadFile(assemblyName);

            List<Type> features =
                typeAssembly.GetTypes().ToList()
                    .Where(tp => !tp.IsInterface &&
                                 !tp.IsAbstract &&
                                 tp.GetInterfaces().Contains(typeof(IFeature)))
                    .Distinct()
                    .ToList();

            builder.Register(c => new FeatureFactory
            {
                Features = (from feature in features
                            select feature.GetInterface("I" + feature.Name)
                            into typeInterface
                            select c.Resolve(typeInterface) as IFeature).ToArray()
            }).As<IFeatureFactory>().InstancePerLifetimeScope();

            var container = builder.Build();

            return container;
        }
    }
}
